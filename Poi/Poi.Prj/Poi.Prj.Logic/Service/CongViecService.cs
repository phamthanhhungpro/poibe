using Microsoft.EntityFrameworkCore;
using Poi.Id.InfraModel.DataAccess.Prj;
using Poi.Prj.InfraModel.DataAccess;
using Poi.Prj.Logic.Dtos;
using Poi.Prj.Logic.Interface;
using Poi.Prj.Logic.Requests;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Constants;
using Poi.Shared.Model.Dtos;
using Poi.Shared.Model.Helpers;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Poi.Prj.Logic.Service
{
    public class CongViecService : ICongViecService
    {
        private readonly PrjDbContext _context;

        public CongViecService(PrjDbContext context)
        {
            _context = context;
        }

        public async Task<CudResponseDto> AddAsync(CongViecRequest request, TenantInfo info)
        {
            var entity = new PrjCongViec
            {
                TenCongViec = request.TenCongViec,
                MoTa = request.MoTa,
                DuAnNvChuyenMonId = request.DuAnNvChuyenMonId,
                NgayBatDau = request.NgayBatDau.ToUTC(),
                NgayKetThuc = request.NgayKetThuc.ToUTC(),
                TrangThai = TrangThaiCongViecHelper.DefaultTrangThaiKey,
                NguoiDuocGiaoId = request.NguoiDuocGiaoId,
                NguoiGiaoViecId = info.UserId,
                CongViecChaId = request.CongViecChaId,
                LoaiCongViecId = request.LoaiCongViecId,
                NhomCongViecId = request.NhomCongViecId,
                TenantId = info.TenantId,
                NguoiPhoiHop = await _context.Users.Where(x => request.NguoiPhoiHopIds.Contains(x.Id)).ToListAsync(),
                NguoiThucHien = await _context.Users.Where(x => request.NguoiThucHienIds.Contains(x.Id)).ToListAsync(),
                TagCongViec = await _context.PrjTagCongViec.Where(x => request.TagCongViecIds.Contains(x.Id)).ToListAsync(),
                ThoiGianDuKien = request.ThoiGianDuKien,
                MucDoUuTien = request.MucDoUuTien,
                Attachments = request.Attachments,
                CreatedBy = info.UserId
            };

            // Kiểm tra người tạo việc có phải là quản lý dự án không
            var duAn = await _context.PrjDuAnNvChuyenMon.FindAsync(request.DuAnNvChuyenMonId);
            if (duAn == null)
            {
                return new CudResponseDto
                {
                    Message = "Không tìm thấy dự án",
                    IsSucceeded = false
                };
            }

            var loaiCongViec = "Tạo công việc";
            if (info.UserId != duAn.QuanLyDuAnId && !duAn.IsCaNhan && (info.RequestScopeCode == null || !info.RequestScopeCode.Contains(ScopeCode.THEM_CONGVIEC_AUTODUYET)))
            {
                entity.TrangThaiChiTiet = TrangThaiCongViecChiTiet.ChoDuyetDeXuat;
                loaiCongViec = "Đề xuất công việc";
            }

            _context.PrjCongViec.Add(entity);

            await _context.SaveChangesAsync();

            // Log hoạt động
            await LogHoatDong(info, entity, loaiCongViec, string.Empty);

            return new CudResponseDto
            {
                Id = entity.Id,
                Message = "Thêm thành công",
                IsSucceeded = true
            };
        }

        public async Task<CudResponseDto> DeleteAsync(Guid id, TenantInfo info)
        {
            Expression<Func<PrjCongViec, bool>> filterExpression = x => true;
            // check scope
            if (info.IsNeedCheckScope && info.RequestScopeCode != null && info.RequestScopeCode.Count > 0)
            {
                // check scope
                var user = await _context.Users
                                        .Include(x => x.LanhDaoPhongBan)
                                        .Include(x => x.ThanhVienPhongBan)
                                        .Include(x => x.LanhDaoToNhom)
                                        .Include(x => x.ThanhVienToNhom)
                                        .FirstOrDefaultAsync(x => x.Id == info.UserId);

                switch (info.RequestScopeCode.First())
                {
                    // Tất cả công việc của đơn vị
                    case ScopeCode.XOA_CONGVIEC_ALL:
                        filterExpression = x => true;
                        break;

                    // Công việc của phòng ban người dùng thuộc về
                    case ScopeCode.XOA_CONGVIEC_DUAN:
                        var lanhDaoPhongBanIds = user?.LanhDaoPhongBan?.Select(pb => pb.Id).ToList() ?? [];
                        var thanhVienPhongBanIds = user?.ThanhVienPhongBan?.Select(pb => pb.Id).ToList() ?? [];
                        var lanhDaoToNhomIds = user?.LanhDaoToNhom?.Select(pb => pb.Id).ToList() ?? [];
                        var thanhVienToNhomIds = user?.ThanhVienToNhom?.Select(pb => pb.Id).ToList() ?? [];

                        filterExpression = x =>
                            x.DuAnNvChuyenMon != null &&
                            (lanhDaoPhongBanIds.Contains(x.DuAnNvChuyenMon.PhongBanBoPhanId.Value) ||
                            thanhVienPhongBanIds.Contains(x.DuAnNvChuyenMon.PhongBanBoPhanId.Value) ||
                            lanhDaoToNhomIds.Contains(x.DuAnNvChuyenMon.ToNhomId.Value) ||
                            thanhVienToNhomIds.Contains(x.DuAnNvChuyenMon.ToNhomId.Value));
                        break;

                    // Chỉ những công việc mà người dùng tạo ra
                    case ScopeCode.XOA_CONGVIEC_CREATED:
                        filterExpression = x => x.CreatedBy == info.UserId;
                        break;

                    default:
                        // Handle other cases here
                        break;
                }
            }

            var entity = await _context.PrjCongViec.Where(filterExpression).FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return new CudResponseDto
                {
                    Message = "Không tìm thấy dữ liệu",
                    IsSucceeded = false
                };
            }

            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow;

            _context.PrjCongViec.Update(entity);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Message = "Xóa thành công",
                IsSucceeded = true
            };
        }

        public async Task<PrjCongViec> GetByIdAsync(Guid id, TenantInfo info)
        {
            return await _context.PrjCongViec
                                            .Include(x => x.NguoiThucHien)
                                            .Include(x => x.NguoiGiaoViec)
                                            .Include(x => x.NguoiPhoiHop)
                                            .Include(x => x.TagCongViec)
                                            .Include(x => x.LoaiCongViec)
                                            .Include(x => x.NhomCongViec)
                                            .Include(x => x.NguoiDuocGiao)
                                            .Include(x => x.LoaiCongViec)
                                            .Include(x => x.DuAnNvChuyenMon).ThenInclude(x => x.ThanhVienDuAn)
                                            .Include(x => x.DuAnNvChuyenMon).ThenInclude(x => x.DuAnSetting)
                                            .Include(x => x.DuAnNvChuyenMon).ThenInclude(x => x.TagComment)
                                            .FirstOrDefaultAsync(x => x.Id == id && x.TenantId == info.TenantId);
        }

        public async Task<IEnumerable<PrjCongViec>> GetNoPaging(TenantInfo info, Guid DuanId)
        {
            return await _context.PrjCongViec
                                            .Include(x => x.NguoiThucHien)
                                            .Include(x => x.NguoiGiaoViec)
                                            .Include(x => x.NguoiPhoiHop)
                                            .Include(x => x.TagCongViec)
                                            .Include(x => x.LoaiCongViec)
                                            .Include(x => x.NhomCongViec)
                                            .Include(x => x.NguoiDuocGiao)
                                            .Include(x => x.DuAnNvChuyenMon)
                                            .Where(x => x.DuAnNvChuyenMonId == DuanId && x.TenantId == info.TenantId)
                                            .ToListAsync();
        }

        public async Task<CudResponseDto> UpdateAsync(Guid id, UpdateCongViecRequest request, TenantInfo info)
        {
            var entity = await _context.PrjCongViec
                                        .Include(x => x.NguoiPhoiHop)
                                        .Include(x => x.NguoiThucHien)
                                        .FirstOrDefaultAsync(x => x.Id == id && x.TenantId == info.TenantId);
            if (entity == null)
            {
                return new CudResponseDto
                {
                    Message = "Không tìm thấy dữ liệu",
                    IsSucceeded = false
                };
            }

            var trangThaiSetting = new List<TrangThaiCongViecSettingDto>();
            var oldTrangThai = string.Empty;
            var newTrangThai = string.Empty;
            if (!string.IsNullOrEmpty(request.TrangThai))
            {
                // get settings
                var settings = await _context.PrjDuAnSetting.FirstOrDefaultAsync(x => x.DuAnNvChuyenMonId == entity.DuAnNvChuyenMonId && x.Key == TrangThaiCongViecHelper.TrangThaiSettingKey);
                if (settings != null)
                {
                    trangThaiSetting = JsonSerializer.Deserialize<List<TrangThaiCongViecSettingDto>>(settings.JsonValue);
                    oldTrangThai = trangThaiSetting.FirstOrDefault(x => x.key == entity.TrangThai)?.value;
                    newTrangThai = trangThaiSetting.FirstOrDefault(x => x.key == request.TrangThai)?.value;

                    var requestTrangThai = trangThaiSetting.FirstOrDefault(x => x.key == request.TrangThai);
                    if (requestTrangThai != null && requestTrangThai.yeuCauXacNhan)
                    {
                        entity.TrangThaiChiTiet = TrangThaiCongViecChiTiet.ChoDuyetHoanThanh;
                        entity.TrangThaiChoXacNhan = entity.TrangThai;
                        entity.TrangThai = request.TrangThai;
                    }
                    else
                    {
                        entity.TrangThai = request.TrangThai;
                    }
                }
            }

            entity.TenCongViec = request.TenCongViec;
            entity.MoTa = request.MoTa;
            entity.NgayBatDau = request.NgayBatDau.ToUTC();
            entity.NhomCongViecId = request.NhomCongViecId;
            entity.LoaiCongViecId = request.LoaiCongViecId;
            entity.ThoiGianDuKien = request.ThoiGianDuKien;
            entity.MucDoUuTien = request.MucDoUuTien;
            entity.TagCongViec = await _context.PrjTagCongViec.Where(x => request.TagCongViecIds.Contains(x.Id)).ToListAsync();

            entity.NgayKetThuc = request.NgayKetThuc.ToUTC();
            entity.NguoiDuocGiaoId = request.NguoiDuocGiaoId;
            entity.NguoiPhoiHop = await _context.Users.Where(x => request.NguoiPhoiHopIds.Contains(x.Id)).ToListAsync();
            entity.NguoiThucHien = await _context.Users.Where(x => request.NguoiThucHienIds.Contains(x.Id)).ToListAsync();

            entity.Attachments = request.Attachments;

            _context.PrjCongViec.Update(entity);
            await _context.SaveChangesAsync();

            // Log hoạt động
            if (oldTrangThai != newTrangThai)
            {
                await LogHoatDong(info, entity, "Cập nhật trạng thái công việc", $"Cập nhật trạng thái từ {oldTrangThai} sang {newTrangThai}");
            }

            await LogHoatDong(info, entity, "Cập nhật thông tin công việc", string.Empty);

            return new CudResponseDto
            {
                Message = "Cập nhật thành công",
                IsSucceeded = true
            };
        }


        public async Task<PagingResponse<CongViecGroupByNhomCongViecDto>> GetCongViecGrid(TenantInfo info, GetCongViecGridRequest request)
        {
            Expression<Func<PrjCongViec, bool>> filterExpression = x => true;
            // check scope
            if (info.IsNeedCheckScope && info.RequestScopeCode != null && info.RequestScopeCode.Count > 0)
            {
                // check scope
                var user = await _context.Users
                                        .Include(x => x.LanhDaoPhongBan)
                                        .Include(x => x.ThanhVienPhongBan)
                                        .Include(x => x.LanhDaoToNhom)
                                        .Include(x => x.ThanhVienToNhom)
                                        .FirstOrDefaultAsync(x => x.Id == info.UserId);

                switch (info.RequestScopeCode.First())
                {
                    // Tất cả công việc của đơn vị
                    case ScopeCode.DANHSACH_CONGVIEC_ALL:
                        filterExpression = x => true;
                        break;

                    // Công việc của phòng ban người dùng thuộc về
                    case ScopeCode.DANHSACH_CONGVIEC_DUAN:
                        var lanhDaoPhongBanIds = user?.LanhDaoPhongBan?.Select(pb => pb.Id).ToList() ?? [];
                        var thanhVienPhongBanIds = user?.ThanhVienPhongBan?.Select(pb => pb.Id).ToList() ?? [];
                        var lanhDaoToNhomIds = user?.LanhDaoToNhom?.Select(pb => pb.Id).ToList() ?? [];
                        var thanhVienToNhomIds = user?.ThanhVienToNhom?.Select(pb => pb.Id).ToList() ?? [];

                        filterExpression = x =>
                            x.DuAnNvChuyenMon != null &&
                            (lanhDaoPhongBanIds.Contains(x.DuAnNvChuyenMon.PhongBanBoPhanId.Value) ||
                            thanhVienPhongBanIds.Contains(x.DuAnNvChuyenMon.PhongBanBoPhanId.Value) ||
                            lanhDaoToNhomIds.Contains(x.DuAnNvChuyenMon.ToNhomId.Value) ||
                            thanhVienToNhomIds.Contains(x.DuAnNvChuyenMon.ToNhomId.Value));
                        break;

                    // Chỉ những công việc mà người dùng liên quan
                    case ScopeCode.DANHSACH_CONGVIEC_RELATED:
                        filterExpression = x => x.NguoiThucHien.Any(tv => tv.Id == info.UserId)
                                             || x.NguoiDuocGiaoId == info.UserId
                                             || x.NguoiGiaoViecId == info.UserId
                                             || x.NguoiPhoiHop.Any(tv => tv.Id == info.UserId);
                        break;

                    default:
                        // Handle other cases here
                        break;
                }
            }

            // Apply additional filters from request
            if (!string.IsNullOrEmpty(request.TaskName))
            {
                filterExpression = filterExpression.AndAlso(x => x.TenCongViec.Contains(request.TaskName));
            }

            if (request.StartDate.HasValue && request.EndDate.HasValue)
            {
                filterExpression = filterExpression.AndAlso(x => x.NgayKetThuc >= request.StartDate.Value.ToUTC() && x.NgayKetThuc <= request.EndDate.Value.ToUTC());
            }

            if (request.AssignedUserIds != null && request.AssignedUserIds.Count > 0)
            {
                filterExpression = filterExpression.AndAlso(x => x.NguoiThucHien.Any(tv => request.AssignedUserIds.Contains(tv.Id)) || request.AssignedUserIds.Contains(x.NguoiDuocGiaoId.Value));
            }

            if (request.Status != null && request.Status.Count > 0)
            {
                filterExpression = filterExpression.AndAlso(x => request.Status.Contains(x.TrangThai));
            }

            // Fetch tasks from the database with the applied filters and pagination (CongViecChaId == null to get only parent tasks)
            var query = _context.PrjCongViec
                                .Include(x => x.NguoiThucHien)
                                .Include(x => x.NguoiGiaoViec)
                                .Include(x => x.NhomCongViec)
                                .Include(x => x.NguoiDuocGiao)
                                .Where(x => x.DuAnNvChuyenMonId == request.DuAnId && x.CongViecChaId == null && x.TenantId == info.TenantId)
                                .Where(filterExpression)
                                .OrderByDescending(x => x.CreatedAt);

            var totalItems = await query.CountAsync();
            var items = await query.Skip(request.PageIndex * request.PageSize).Take(request.PageSize).ToListAsync();

            var result = items.GroupBy(x => x.NhomCongViecId)
                              .Select(x => new CongViecGroupByNhomCongViecDto
                              {
                                  NhomCongViecId = x.Key,
                                  TenNhomCongViec = x.FirstOrDefault().NhomCongViec?.TenNhomCongViec,
                                  ListCongViec = x.Select(y => new CongViecGridDto
                                  {
                                      Id = y.Id,
                                      TenCongViec = y.TenCongViec,
                                      MoTa = y.MoTa,
                                      NgayBatDau = y.NgayBatDau.ToLocalTime(),
                                      NgayKetThuc = y.NgayKetThuc.ToLocalTime(),
                                      TrangThai = y.TrangThai,
                                      NguoiDuocGiao = y.NguoiDuocGiao,
                                      NguoiGiaoViec = y.NguoiGiaoViec,
                                      CreatedAt = y.CreatedAt.ToLocalTime().ToString("dd/MM/yyyy HH:mm"),
                                      TrangThaiChiTiet = y.TrangThaiChiTiet
                                  })
                                  .OrderByDescending(x => x.CreatedAt)
                                  .ToList()
                              });

            return new PagingResponse<CongViecGroupByNhomCongViecDto>
            {
                Count = totalItems,
                Items = result.ToList()
            };
        }

        public async Task<IEnumerable<CongViecKanbanDto>> GetCongViecKanban(TenantInfo info, Guid DuanId)
        {
            Expression<Func<PrjCongViec, bool>> filterExpression = x => true;
            // check scope
            if (info.IsNeedCheckScope && info.RequestScopeCode != null && info.RequestScopeCode.Count > 0)
            {
                // check scope
                var user = await _context.Users
                                        .Include(x => x.LanhDaoPhongBan)
                                        .Include(x => x.ThanhVienPhongBan)
                                        .Include(x => x.LanhDaoToNhom)
                                        .Include(x => x.ThanhVienToNhom)
                                        .FirstOrDefaultAsync(x => x.Id == info.UserId);

                switch (info.RequestScopeCode.First())
                {
                    // Tất cả công việc của đơn vị
                    case ScopeCode.DANHSACH_CONGVIEC_ALL:
                        filterExpression = x => true;
                        break;

                    // Công việc của phòng ban người dùng thuộc về
                    case ScopeCode.DANHSACH_CONGVIEC_DUAN:
                        var lanhDaoPhongBanIds = user?.LanhDaoPhongBan?.Select(pb => pb.Id).ToList() ?? [];
                        var thanhVienPhongBanIds = user?.ThanhVienPhongBan?.Select(pb => pb.Id).ToList() ?? [];
                        var lanhDaoToNhomIds = user?.LanhDaoToNhom?.Select(pb => pb.Id).ToList() ?? [];
                        var thanhVienToNhomIds = user?.ThanhVienToNhom?.Select(pb => pb.Id).ToList() ?? [];

                        filterExpression = x =>
                            x.DuAnNvChuyenMon != null &&
                            (lanhDaoPhongBanIds.Contains(x.DuAnNvChuyenMon.PhongBanBoPhanId.Value) ||
                            thanhVienPhongBanIds.Contains(x.DuAnNvChuyenMon.PhongBanBoPhanId.Value) ||
                            lanhDaoToNhomIds.Contains(x.DuAnNvChuyenMon.ToNhomId.Value) ||
                            thanhVienToNhomIds.Contains(x.DuAnNvChuyenMon.ToNhomId.Value));
                        break;

                    // Chỉ những công việc mà người dùng liên quan
                    case ScopeCode.DANHSACH_CONGVIEC_RELATED:
                        filterExpression = x => x.NguoiThucHien.Any(tv => tv.Id == info.UserId)
                                             || x.NguoiDuocGiaoId == info.UserId
                                             || x.NguoiGiaoViecId == info.UserId
                                             || x.NguoiPhoiHop.Any(tv => tv.Id == info.UserId);
                        break;

                    default:
                        // Handle other cases here
                        break;
                }
            }
            // Lấy danh sách cột kanban của dự án
            var listKanban = await _context.PrjKanban
                                            .Where(x => x.DuAnNvChuyenMonId == DuanId)
                                            .OrderBy(x => x.ThuTu)
                                            .ToListAsync();

            // Lấy danh sách công việc của dự án
            var listCongViec = await _context.PrjCongViec
                                            .Include(x => x.NguoiThucHien)
                                            .Include(x => x.NguoiDuocGiao)
                                            .Include(x => x.NguoiGiaoViec)
                                            .Include(x => x.NhomCongViec)
                                            .Include(x => x.TagCongViec)
                                            .Where(x => x.DuAnNvChuyenMonId == DuanId)
                                            .Where(filterExpression)
                                            .ToListAsync();

            // Group công việc theo cột kanban
            var result = listKanban.Select(x => new CongViecKanbanDto
            {
                Id = x.Id,
                TenCot = x.TenCot,
                MoTa = x.MoTa,
                YeuCauXacNhan = x.YeuCauXacNhan,
                ThuTu = x.ThuTu,
                TrangThaiCongViec = x.TrangThaiCongViec,
                ListCongViec = listCongViec.Where(y => y.TrangThai == x.TrangThaiCongViec).Select(y => new CongViecGridDto
                {
                    Id = y.Id,
                    TenCongViec = y.TenCongViec,
                    MoTa = y.MoTa,
                    NgayBatDau = y.NgayBatDau.ToLocalTime(),
                    NgayKetThuc = y.NgayKetThuc.ToLocalTime(),
                    TrangThai = y.TrangThai,
                    NguoiDuocGiao = y.NguoiDuocGiao,
                    NguoiGiaoViec = y.NguoiGiaoViec,
                    TagCongViec = y.TagCongViec,
                    CreatedAt = y.CreatedAt.ToLocalTime().ToString("dd/MM/yyyy HH:mm"),
                    TrangThaiChiTiet = y.TrangThaiChiTiet
                }).ToList()
            });

            return result;
        }

        public async Task<CudResponseDto> UpdateKanbanStatus(TenantInfo info, UpdateCongViecKanbanStatusRequest request)
        {
            string message = "";
            // Get kanban column
            var kanban = await _context.PrjKanban.FindAsync(request.IdKanban);
            if (kanban == null)
            {
                return new CudResponseDto
                {
                    IsSucceeded = false,
                    Message = "Không tìm thấy cột kanban"
                };
            }

            // Get cong viec
            var congViec = await _context.PrjCongViec.FindAsync(request.IdCongViec);
            if (congViec == null)
            {
                return new CudResponseDto
                {
                    IsSucceeded = false,
                    Message = "Không tìm thấy công việc"
                };
            }

            var trangThaiSetting = new List<TrangThaiCongViecSettingDto>();
            var oldTrangThai = string.Empty;
            var newTrangThai = string.Empty;
            if (!string.IsNullOrEmpty(kanban.TrangThaiCongViec))
            {
                // get settings
                var settings = await _context.PrjDuAnSetting.FirstOrDefaultAsync(x => x.DuAnNvChuyenMonId == congViec.DuAnNvChuyenMonId
                                                                                   && x.Key == TrangThaiCongViecHelper.TrangThaiSettingKey);
                if (settings != null)
                {
                    trangThaiSetting = JsonSerializer.Deserialize<List<TrangThaiCongViecSettingDto>>(settings.JsonValue);
                    oldTrangThai = trangThaiSetting.FirstOrDefault(x => x.key == congViec.TrangThai)?.value;
                    newTrangThai = trangThaiSetting.FirstOrDefault(x => x.key == kanban.TrangThaiCongViec)?.value;

                    var requestTrangThai = trangThaiSetting.FirstOrDefault(x => x.key == kanban.TrangThaiCongViec);
                    if (requestTrangThai != null && requestTrangThai.yeuCauXacNhan)
                    {
                        message = "Chờ xác nhận để hoàn thành công việc";
                        congViec.TrangThaiChiTiet = TrangThaiCongViecChiTiet.ChoDuyetHoanThanh;
                        congViec.TrangThaiChoXacNhan = congViec.TrangThai;
                        congViec.TrangThai = kanban.TrangThaiCongViec;
                    }
                    else
                    {
                        message = "Cập nhật trạng thái công việc thành công";
                        congViec.TrangThai = kanban.TrangThaiCongViec;
                    }
                }
            }
            // Update trang thai cong viec
            _context.PrjCongViec.Update(congViec);

            await _context.SaveChangesAsync();

            if (oldTrangThai != newTrangThai)
            {
                // Log hoạt động
                await LogHoatDong(info, congViec, "Cập nhật trạng thái công việc", $"Cập nhật trạng thái từ {oldTrangThai} sang {newTrangThai}");
            }

            return new CudResponseDto
            {
                IsSucceeded = true,
                Message = message
            };
        }

        public async Task<CudResponseDto> GiaHanCongViec(TenantInfo info, GiaHanCongViecRequest request)
        {
            var entity = await _context.PrjCongViec.FindAsync(request.Id);
            if (entity == null)
            {
                return new CudResponseDto
                {
                    Message = "Không tìm thấy công việc",
                    IsSucceeded = false
                };
            }

            if (request.NgayKetThuc < entity.NgayKetThuc)
            {
                return new CudResponseDto
                {
                    Message = "Ngày gia hạn phải lớn hơn ngày kết thúc hiện tại",
                    IsSucceeded = false
                };
            }

            var oldNgayKetThuc = entity.NgayKetThuc.ToString("dd/MM/yyyy");

            entity.NgayGiaHan = request.NgayKetThuc.ToUTC();
            entity.TrangThaiChiTiet = TrangThaiCongViecChiTiet.ChoDuyetGiaHan;

            _context.PrjCongViec.Update(entity);
            await _context.SaveChangesAsync();


            // Log hoạt động
            await LogHoatDong(info, entity, "Yêu cầu gia hạn", $"Yêu cầu gia hạn từ {oldNgayKetThuc} sang {entity.NgayGiaHan.Value:dd/MM/yyyy}");

            return new CudResponseDto
            {
                Message = "Gia hạn công việc thành công",
                IsSucceeded = true
            };
        }

        public async Task<IEnumerable<CongViecGroupByNhomCongViecDto>> GetCongViecGridByTrangThai(TenantInfo info, Guid DuanId, string TrangThai)
        {
            var listCongViec = await _context.PrjCongViec
                                                .Include(x => x.NguoiThucHien)
                                                .Include(x => x.NguoiGiaoViec)
                                                .Include(x => x.NhomCongViec)
                                                .Include(x => x.NguoiDuocGiao)
                                                .Where(x => x.DuAnNvChuyenMonId == DuanId && x.TrangThaiChiTiet == TrangThai && x.TenantId == info.TenantId)
                                                .ToListAsync();

            // Group công việc cha theo nhóm công việc
            var result = listCongViec.GroupBy(x => x.NhomCongViecId)
                                        .Select(x => new CongViecGroupByNhomCongViecDto
                                        {
                                            NhomCongViecId = x.Key,
                                            TenNhomCongViec = x.FirstOrDefault().NhomCongViec?.TenNhomCongViec,
                                            ListCongViec = x.Select(y => new CongViecGridDto
                                            {
                                                Id = y.Id,
                                                TenCongViec = y.TenCongViec,
                                                MoTa = y.MoTa,
                                                NgayBatDau = y.NgayBatDau.ToLocalTime(),
                                                NgayKetThuc = y.NgayKetThuc.ToLocalTime(),
                                                NgayGiaHan = y.NgayGiaHan?.ToLocalTime(),
                                                TrangThai = y.TrangThai,
                                                NguoiDuocGiao = y.NguoiDuocGiao,
                                                NguoiGiaoViec = y.NguoiGiaoViec,
                                                CreatedAt = y.CreatedAt.ToLocalTime().ToString("dd/MM/yyyy HH:mm"),
                                                TrangThaiChiTiet = y.TrangThaiChiTiet
                                            }).ToList()
                                        });

            return result;
        }

        public async Task<CudResponseDto> ApproveGiaHanCongViec(TenantInfo info, ApproveGiaHanCongViec request)
        {
            Expression<Func<PrjCongViec, bool>> filterExpression = x => true;
            // check scope
            if (info.IsNeedCheckScope && info.RequestScopeCode != null && info.RequestScopeCode.Count > 0)
            {
                // check scope
                var user = await _context.Users
                                        .Include(x => x.LanhDaoPhongBan)
                                        .Include(x => x.ThanhVienPhongBan)
                                        .Include(x => x.LanhDaoToNhom)
                                        .Include(x => x.ThanhVienToNhom)
                                        .FirstOrDefaultAsync(x => x.Id == info.UserId);

                switch (info.RequestScopeCode.First())
                {
                    // Tất cả dự án của đơn vị
                    case ScopeCode.DUYET_CONGVIEC_ALL:
                        filterExpression = x => true;
                        break;

                    // Duyệt phòng ban tổ nhóm mình phụ trách
                    case ScopeCode.DUYET_CONGVIEC_DUAN:
                        var lanhDaoPhongBanIds = user?.LanhDaoPhongBan?.Select(pb => pb.Id).ToList() ?? [];
                        var lanhDaoToNhomIds = user?.LanhDaoToNhom?.Select(pb => pb.Id).ToList() ?? [];

                        filterExpression = x => x.DuAnNvChuyenMon != null && x.DuAnNvChuyenMon.QuanLyDuAnId.HasValue &&
                            (lanhDaoPhongBanIds.Contains(x.DuAnNvChuyenMon.QuanLyDuAnId.Value) ||
                            lanhDaoToNhomIds.Contains(x.DuAnNvChuyenMon.QuanLyDuAnId.Value));
                        break;


                    default:
                        // Handle other cases here
                        break;
                }
            }
            // Get cong viec
            var result = await _context.PrjCongViec
                .Where(x => request.CongViecIds.Contains(x.Id))
                .Where(filterExpression)
                .ExecuteUpdateAsync(x => x.SetProperty(p => p.NgayKetThuc, p => p.NgayGiaHan)
                                          .SetProperty(p => p.TrangThaiChiTiet, TrangThaiCongViecChiTiet.READY));

            if (result == 0)
            {
                return new CudResponseDto
                {
                    IsSucceeded = false,
                    Message = "Duyệt gia hạn công việc không thành công"
                };
            }
            else
            {
                // Log hoạt động
                var listCongViec = await _context.PrjCongViec.Where(x => request.CongViecIds.Contains(x.Id)).ToListAsync();
                foreach (var item in listCongViec)
                {
                    await LogHoatDong(info, item, "Duyệt gia hạn công việc", string.Empty);
                }

                return new CudResponseDto
                {
                    IsSucceeded = true,
                    Message = "Duyệt gia hạn công việc thành công"
                };
            }
        }

        public async Task<CudResponseDto> ApproveTrangThaiCongViec(TenantInfo info, ApproveTrangThaiCongViec request)
        {
            Expression<Func<PrjCongViec, bool>> filterExpression = x => true;
            // check scope
            if (info.IsNeedCheckScope && info.RequestScopeCode != null && info.RequestScopeCode.Count > 0)
            {
                // check scope
                var user = await _context.Users
                                        .Include(x => x.LanhDaoPhongBan)
                                        .Include(x => x.ThanhVienPhongBan)
                                        .Include(x => x.LanhDaoToNhom)
                                        .Include(x => x.ThanhVienToNhom)
                                        .FirstOrDefaultAsync(x => x.Id == info.UserId);

                switch (info.RequestScopeCode.First())
                {
                    // Tất cả dự án của đơn vị
                    case ScopeCode.DUYET_CONGVIEC_ALL:
                        filterExpression = x => true;
                        break;

                    // Duyệt phòng ban tổ nhóm mình phụ trách
                    case ScopeCode.DUYET_CONGVIEC_DUAN:
                        var lanhDaoPhongBanIds = user?.LanhDaoPhongBan?.Select(pb => pb.Id).ToList() ?? [];
                        var lanhDaoToNhomIds = user?.LanhDaoToNhom?.Select(pb => pb.Id).ToList() ?? [];

                        filterExpression = x => x.DuAnNvChuyenMon != null && x.DuAnNvChuyenMon.QuanLyDuAnId.HasValue &&
                            (lanhDaoPhongBanIds.Contains(x.DuAnNvChuyenMon.QuanLyDuAnId.Value) ||
                            lanhDaoToNhomIds.Contains(x.DuAnNvChuyenMon.QuanLyDuAnId.Value));
                        break;


                    default:
                        // Handle other cases here
                        break;
                }
            }
            var result = await _context.PrjCongViec
                .Where(x => request.CongViecIds.Contains(x.Id))
                .Where(filterExpression)
                .ExecuteUpdateAsync(x => x.SetProperty(p => p.TrangThai, p => p.TrangThai)
                                          .SetProperty(p => p.TrangThaiChiTiet, TrangThaiCongViecChiTiet.READY));

            if (result == 0)
            {
                return new CudResponseDto
                {
                    IsSucceeded = false,
                    Message = "Duyệt thay đổi trạng thái công việc không thành công"
                };
            }
            else
            {
                // Log hoạt động
                var listCongViec = await _context.PrjCongViec.Where(x => request.CongViecIds.Contains(x.Id)).ToListAsync();
                foreach (var item in listCongViec)
                {
                    await LogHoatDong(info, item, "Duyệt thay đổi trạng thái công việc", string.Empty);
                }
                return new CudResponseDto
                {
                    IsSucceeded = true,
                    Message = "Duyệt trạng thái công việc thành công"
                };
            }
        }

        public async Task<CudResponseDto> ApproveDeXuatCongViec(TenantInfo info, ApproveDeXuatCongViec request)
        {
            Expression<Func<PrjCongViec, bool>> filterExpression = x => true;
            // check scope
            if (info.IsNeedCheckScope && info.RequestScopeCode != null && info.RequestScopeCode.Count > 0)
            {
                // check scope
                var user = await _context.Users
                                        .Include(x => x.LanhDaoPhongBan)
                                        .Include(x => x.ThanhVienPhongBan)
                                        .Include(x => x.LanhDaoToNhom)
                                        .Include(x => x.ThanhVienToNhom)
                                        .FirstOrDefaultAsync(x => x.Id == info.UserId);

                switch (info.RequestScopeCode.First())
                {
                    // Tất cả dự án của đơn vị
                    case ScopeCode.DUYET_CONGVIEC_ALL:
                        filterExpression = x => true;
                        break;

                    // Duyệt phòng ban tổ nhóm mình phụ trách
                    case ScopeCode.DUYET_CONGVIEC_DUAN:
                        var lanhDaoPhongBanIds = user?.LanhDaoPhongBan?.Select(pb => pb.Id).ToList() ?? [];
                        var lanhDaoToNhomIds = user?.LanhDaoToNhom?.Select(pb => pb.Id).ToList() ?? [];

                        filterExpression = x => x.DuAnNvChuyenMon != null && x.DuAnNvChuyenMon.QuanLyDuAnId.HasValue &&
                            (lanhDaoPhongBanIds.Contains(x.DuAnNvChuyenMon.QuanLyDuAnId.Value) ||
                            lanhDaoToNhomIds.Contains(x.DuAnNvChuyenMon.QuanLyDuAnId.Value));
                        break;


                    default:
                        // Handle other cases here
                        break;
                }
            }

            // Get cong viec
            var result = await _context.PrjCongViec
                .Where(x => request.CongViecIds.Contains(x.Id))
                .Where(filterExpression)
                .ExecuteUpdateAsync(x => x.SetProperty(p => p.TrangThaiChiTiet, TrangThaiCongViecChiTiet.READY));

            if (result == 0)
            {
                return new CudResponseDto
                {
                    IsSucceeded = false,
                    Message = "Duyệt đề xuất công việc không thành công"
                };
            }
            else
            {
                // Log hoạt động
                var listCongViec = await _context.PrjCongViec.Where(x => request.CongViecIds.Contains(x.Id)).ToListAsync();
                foreach (var item in listCongViec)
                {
                    await LogHoatDong(info, item, "Duyệt công việc đề xuất", string.Empty);
                }

                return new CudResponseDto
                {
                    IsSucceeded = true,
                    Message = "Duyệt đề xuất công việc thành công"
                };
            }
        }

        public async Task<CudResponseDto> RejectDeXuatCongViec(TenantInfo info, RejectCongViecRequest request)
        {
            Expression<Func<PrjCongViec, bool>> filterExpression = x => true;
            // check scope
            if (info.IsNeedCheckScope && info.RequestScopeCode != null && info.RequestScopeCode.Count > 0)
            {
                // check scope
                var user = await _context.Users
                                        .Include(x => x.LanhDaoPhongBan)
                                        .Include(x => x.ThanhVienPhongBan)
                                        .Include(x => x.LanhDaoToNhom)
                                        .Include(x => x.ThanhVienToNhom)
                                        .FirstOrDefaultAsync(x => x.Id == info.UserId);

                switch (info.RequestScopeCode.First())
                {
                    // Tất cả dự án của đơn vị
                    case ScopeCode.DUYET_CONGVIEC_ALL:
                        filterExpression = x => true;
                        break;

                    // Duyệt phòng ban tổ nhóm mình phụ trách
                    case ScopeCode.DUYET_CONGVIEC_DUAN:
                        var lanhDaoPhongBanIds = user?.LanhDaoPhongBan?.Select(pb => pb.Id).ToList() ?? [];
                        var lanhDaoToNhomIds = user?.LanhDaoToNhom?.Select(pb => pb.Id).ToList() ?? [];

                        filterExpression = x => x.DuAnNvChuyenMon != null && x.DuAnNvChuyenMon.QuanLyDuAnId.HasValue &&
                            (lanhDaoPhongBanIds.Contains(x.DuAnNvChuyenMon.QuanLyDuAnId.Value) ||
                            lanhDaoToNhomIds.Contains(x.DuAnNvChuyenMon.QuanLyDuAnId.Value));
                        break;


                    default:
                        // Handle other cases here
                        break;
                }
            }


            // Get cong viec
            var result = await _context.PrjCongViec
                .Where(x => request.CongViecIds.Contains(x.Id))
                .Where(filterExpression)
                .ExecuteUpdateAsync(x => x.SetProperty(p => p.TrangThaiChiTiet, TrangThaiCongViecChiTiet.REJECT));

            if (result == 0)
            {
                return new CudResponseDto
                {
                    IsSucceeded = false,
                    Message = "Từ chối đề xuất công việc không thành công"
                };
            }
            else
            {
                // Log hoạt động
                var listCongViec = await _context.PrjCongViec.Where(x => request.CongViecIds.Contains(x.Id)).ToListAsync();
                foreach (var item in listCongViec)
                {
                    await LogHoatDong(info, item, "Từ chối việc đề xuất", string.Empty);
                }

                return new CudResponseDto
                {
                    IsSucceeded = true,
                    Message = "Từ chối đề xuất công việc thành công"
                };
            }

        }

        public async Task<CudResponseDto> RejectGiaHanCongViec(TenantInfo info, RejectCongViecRequest request)
        {
            Expression<Func<PrjCongViec, bool>> filterExpression = x => true;
            // check scope
            if (info.IsNeedCheckScope && info.RequestScopeCode != null && info.RequestScopeCode.Count > 0)
            {
                // check scope
                var user = await _context.Users
                                        .Include(x => x.LanhDaoPhongBan)
                                        .Include(x => x.ThanhVienPhongBan)
                                        .Include(x => x.LanhDaoToNhom)
                                        .Include(x => x.ThanhVienToNhom)
                                        .FirstOrDefaultAsync(x => x.Id == info.UserId);

                switch (info.RequestScopeCode.First())
                {
                    // Tất cả dự án của đơn vị
                    case ScopeCode.DUYET_CONGVIEC_ALL:
                        filterExpression = x => true;
                        break;

                    // Duyệt phòng ban tổ nhóm mình phụ trách
                    case ScopeCode.DUYET_CONGVIEC_DUAN:
                        var lanhDaoPhongBanIds = user?.LanhDaoPhongBan?.Select(pb => pb.Id).ToList() ?? [];
                        var lanhDaoToNhomIds = user?.LanhDaoToNhom?.Select(pb => pb.Id).ToList() ?? [];

                        filterExpression = x => x.DuAnNvChuyenMon != null && x.DuAnNvChuyenMon.QuanLyDuAnId.HasValue &&
                            (lanhDaoPhongBanIds.Contains(x.DuAnNvChuyenMon.QuanLyDuAnId.Value) ||
                            lanhDaoToNhomIds.Contains(x.DuAnNvChuyenMon.QuanLyDuAnId.Value));
                        break;


                    default:
                        // Handle other cases here
                        break;
                }
            }


            // Get cong viec
            var result = await _context.PrjCongViec
                .Where(x => request.CongViecIds.Contains(x.Id))
                .Where(filterExpression)
                .ExecuteUpdateAsync(x => x.SetProperty(p => p.TrangThaiChiTiet, TrangThaiCongViecChiTiet.READY)
                                          .SetProperty(p => p.NgayGiaHan.Value, null));

            if (result == 0)
            {
                return new CudResponseDto
                {
                    IsSucceeded = false,
                    Message = "Từ chối gia hạn công việc không thành công"
                };
            }
            else
            {
                // Log hoạt động
                var listCongViec = await _context.PrjCongViec.Where(x => request.CongViecIds.Contains(x.Id)).ToListAsync();
                foreach (var item in listCongViec)
                {
                    await LogHoatDong(info, item, "Từ chối gia hạn công việc", string.Empty);
                }
                return new CudResponseDto
                {
                    IsSucceeded = true,
                    Message = "Từ chối gia hạn công việc thành công"
                };
            }
        }

        public async Task<CudResponseDto> RejectTrangThaiCongViec(TenantInfo info, RejectCongViecRequest request)
        {
            Expression<Func<PrjCongViec, bool>> filterExpression = x => true;
            // check scope
            if (info.IsNeedCheckScope && info.RequestScopeCode != null && info.RequestScopeCode.Count > 0)
            {
                // check scope
                var user = await _context.Users
                                        .Include(x => x.LanhDaoPhongBan)
                                        .Include(x => x.ThanhVienPhongBan)
                                        .Include(x => x.LanhDaoToNhom)
                                        .Include(x => x.ThanhVienToNhom)
                                        .FirstOrDefaultAsync(x => x.Id == info.UserId);

                switch (info.RequestScopeCode.First())
                {
                    // Tất cả dự án của đơn vị
                    case ScopeCode.DUYET_CONGVIEC_ALL:
                        filterExpression = x => true;
                        break;

                    // Duyệt phòng ban tổ nhóm mình phụ trách
                    case ScopeCode.DUYET_CONGVIEC_DUAN:
                        var lanhDaoPhongBanIds = user?.LanhDaoPhongBan?.Select(pb => pb.Id).ToList() ?? [];
                        var lanhDaoToNhomIds = user?.LanhDaoToNhom?.Select(pb => pb.Id).ToList() ?? [];

                        filterExpression = x => x.DuAnNvChuyenMon != null && x.DuAnNvChuyenMon.QuanLyDuAnId.HasValue &&
                            (lanhDaoPhongBanIds.Contains(x.DuAnNvChuyenMon.QuanLyDuAnId.Value) ||
                            lanhDaoToNhomIds.Contains(x.DuAnNvChuyenMon.QuanLyDuAnId.Value));
                        break;


                    default:
                        // Handle other cases here
                        break;
                }
            }

            // Get cong viec
            var result = await _context.PrjCongViec
                .Where(x => request.CongViecIds.Contains(x.Id))
                .Where(filterExpression)
                .ExecuteUpdateAsync(x => x.SetProperty(p => p.TrangThaiChiTiet, TrangThaiCongViecChiTiet.READY)
                                          .SetProperty(p => p.TrangThai, t => t.TrangThaiChiTiet));

            if (result == 0)
            {
                return new CudResponseDto
                {
                    IsSucceeded = false,
                    Message = "Từ chối thay đổi trạng thái công việc không thành công"
                };
            }
            else
            {
                // Log hoạt động
                var listCongViec = await _context.PrjCongViec.Where(x => request.CongViecIds.Contains(x.Id)).ToListAsync();
                foreach (var item in listCongViec)
                {
                    await LogHoatDong(info, item, "Từ chối thay đổi trạng thái công việc", string.Empty);
                }
                return new CudResponseDto
                {
                    IsSucceeded = true,
                    Message = "Từ chối thay đổi trạng thái công việc thành công"
                };
            }
        }

        public async Task<IEnumerable<CongViecHoatDongDto>> GetCongViecHoatDong(TenantInfo info, Guid CongViecId)
        {
            var data = await _context.PrjHoatDong
                                .Where(x => x.CongViecId == CongViecId && x.TenantId == info.TenantId)
                                .OrderByDescending(x => x.CreatedAt)
                                .Select(x => new CongViecHoatDongDto
                                {
                                    NoiDung = x.NoiDung,
                                    UserName = x.UserName,
                                    ThoiGian = x.CreatedAt.ToLocalTime().ToString("dd/MM/yyyy HH:mm"),
                                    MoreInfo = x.MoreInfo
                                })
                                .ToListAsync();

            var listUserNames = data.Select(x => x.UserName).Distinct().ToList();
            var listUsers = _context.Users.Where(x => listUserNames.Contains(x.UserName)).ToList();

            foreach (var item in data)
            {
                if (item.UserName == null)
                {
                    continue;
                }
                item.UserFullName = listUsers.FirstOrDefault(x => x.UserName == item.UserName).FullName;
            };

            return data;
        }

        private async Task LogHoatDong(TenantInfo info, PrjCongViec congViec, string noiDung, string moreInfo)
        {
            // Log hoạt động
            var userName = await _context.Users.Where(x => x.Id == info.UserId).Select(x => x.UserName).FirstOrDefaultAsync();
            var hoatDong = new PrjHoatDong
            {
                NoiDung = noiDung,
                TenantId = info.TenantId,
                CongViecId = congViec.Id,
                UserName = userName,
                DuanNvChuyenMonId = congViec.DuAnNvChuyenMonId,
                MoreInfo = moreInfo
            };

            _context.PrjHoatDong.Add(hoatDong);
            await _context.SaveChangesAsync();
        }

        public async Task<PagingResponse<CongViecGridDto>> GetQuanLyCongViec(TenantInfo info, GetQuanLyCongViecRequest request)
        {
            Expression<Func<PrjCongViec, bool>> filterExpression = x => true;
            // check scope
            if (info.IsNeedCheckScope && info.RequestScopeCode != null && info.RequestScopeCode.Count > 0)
            {
                // check scope
                var user = await _context.Users
                                        .Include(x => x.LanhDaoPhongBan)
                                        .Include(x => x.ThanhVienPhongBan)
                                        .Include(x => x.LanhDaoToNhom)
                                        .Include(x => x.ThanhVienToNhom)
                                        .FirstOrDefaultAsync(x => x.Id == info.UserId);

                switch (info.RequestScopeCode.First())
                {
                    // Tất cả công việc của đơn vị
                    case ScopeCode.DANHSACH_CONGVIEC_ALL:
                        filterExpression = x => true;
                        break;

                    // Công việc của phòng ban người dùng thuộc về
                    case ScopeCode.DANHSACH_CONGVIEC_DUAN:
                        var lanhDaoPhongBanIds = user?.LanhDaoPhongBan?.Select(pb => pb.Id).ToList() ?? [];
                        var thanhVienPhongBanIds = user?.ThanhVienPhongBan?.Select(pb => pb.Id).ToList() ?? [];
                        var lanhDaoToNhomIds = user?.LanhDaoToNhom?.Select(pb => pb.Id).ToList() ?? [];
                        var thanhVienToNhomIds = user?.ThanhVienToNhom?.Select(pb => pb.Id).ToList() ?? [];

                        filterExpression = x =>
                            x.DuAnNvChuyenMon != null &&
                            (lanhDaoPhongBanIds.Contains(x.DuAnNvChuyenMon.PhongBanBoPhanId.Value) ||
                            thanhVienPhongBanIds.Contains(x.DuAnNvChuyenMon.PhongBanBoPhanId.Value) ||
                            lanhDaoToNhomIds.Contains(x.DuAnNvChuyenMon.ToNhomId.Value) ||
                            thanhVienToNhomIds.Contains(x.DuAnNvChuyenMon.ToNhomId.Value));
                        break;

                    // Chỉ những công việc mà người dùng liên quan
                    case ScopeCode.DANHSACH_CONGVIEC_RELATED:
                        filterExpression = x => x.NguoiThucHien.Any(tv => tv.Id == info.UserId)
                                             || x.NguoiDuocGiaoId == info.UserId
                                             || x.NguoiGiaoViecId == info.UserId
                                             || x.NguoiPhoiHop.Any(tv => tv.Id == info.UserId);
                        break;

                    default:
                        // Handle other cases here
                        break;
                }
            }

            // Apply additional filters from request
            if (!string.IsNullOrEmpty(request.TaskName))
            {
                filterExpression = filterExpression.AndAlso(x => x.TenCongViec.Contains(request.TaskName));
            }

            if (request.StartDate.HasValue && request.EndDate.HasValue)
            {
                filterExpression = filterExpression.AndAlso(x => x.NgayKetThuc >= request.StartDate.Value.ToUTC() && x.NgayKetThuc <= request.EndDate.Value.ToUTC());
            }

            if (request.AssignedUserIds != null && request.AssignedUserIds.Count > 0)
            {
                filterExpression = filterExpression.AndAlso(x => x.NguoiThucHien.Any(tv => request.AssignedUserIds.Contains(tv.Id)) || request.AssignedUserIds.Contains(x.NguoiDuocGiaoId.Value));
            }

            if (request.Status != null && request.Status.Count > 0)
            {
                filterExpression = filterExpression.AndAlso(x => request.Status.Contains(x.TrangThai));
            }

            if (request.DuAnIds != null && request.DuAnIds.Count > 0)
            {
                filterExpression = filterExpression.AndAlso(x => request.DuAnIds.Contains(x.DuAnNvChuyenMonId));
            }

            // Fetch tasks from the database with the applied filters and pagination (CongViecChaId == null to get only parent tasks)
            var query = _context.PrjCongViec
                                .Include(x => x.NguoiThucHien)
                                .Include(x => x.NguoiGiaoViec)
                                .Include(x => x.NhomCongViec)
                                .Include(x => x.NguoiDuocGiao)
                                .Include(x => x.DuAnNvChuyenMon)
                                .Where(x => x.CongViecChaId == null && x.TenantId == info.TenantId && (x.DuAnNvChuyenMon == null || !x.DuAnNvChuyenMon.IsCaNhan))
                                .Where(filterExpression)
                                .OrderByDescending(x => x.CreatedAt);

            var totalItems = await query.CountAsync();
            var items = await query.Skip(request.PageIndex * request.PageSize).Take(request.PageSize).ToListAsync();

            var listDuAnIds = items.Select(x => x.DuAnNvChuyenMonId).ToList();
            // list settings
            var listSettings = await _context.PrjDuAnSetting.Where(x => listDuAnIds.Contains(x.DuAnNvChuyenMonId) && x.Key == TrangThaiCongViecHelper.TrangThaiSettingKey).ToListAsync();
            var listTrangThaiSetting = new Dictionary<Guid, List<TrangThaiCongViecSettingDto>>();
            foreach (var item in listSettings)
            {
                listTrangThaiSetting.Add(item.DuAnNvChuyenMonId, JsonSerializer.Deserialize<List<TrangThaiCongViecSettingDto>>(item.JsonValue));
            }

            foreach (var item in items)
            {
                if (listTrangThaiSetting.ContainsKey(item.DuAnNvChuyenMonId))
                {
                    var trangThaiSetting = listTrangThaiSetting[item.DuAnNvChuyenMonId];
                    item.TrangThai = trangThaiSetting.FirstOrDefault(x => x.key == item.TrangThai)?.value;
                }
            }

            var result = items
                .Select(y => new CongViecGridDto
                {
                    Id = y.Id,
                    TenCongViec = y.TenCongViec,
                    MoTa = y.MoTa,
                    NgayBatDau = y.NgayBatDau.ToLocalTime(),
                    NgayKetThuc = y.NgayKetThuc.ToLocalTime(),
                    TrangThai = y.TrangThai,
                    NguoiDuocGiao = y.NguoiDuocGiao,
                    NguoiGiaoViec = y.NguoiGiaoViec,
                    CreatedAt = y.CreatedAt.ToLocalTime().ToString("dd/MM/yyyy HH:mm"),
                    TrangThaiChiTiet = y.TrangThaiChiTiet,
                    TenDuAn = y.DuAnNvChuyenMon?.TenDuAn,
                })
                .OrderByDescending(x => x.CreatedAt)
                .ThenBy(x => x.TenDuAn)
                .ToList();

            return new PagingResponse<CongViecGridDto>
            {
                Count = totalItems,
                Items = result
            };
        }

        public async Task<CudResponseDto> DanhGiaCongViec(TenantInfo info, DanhGiaCongViecRequest request)
        {
            var entity = await _context.PrjCongViec.FindAsync(request.CongViecId);

            if (entity == null)
            {
                return new CudResponseDto
                {
                    Message = "Không tìm thấy công việc",
                    IsSucceeded = false
                };
            }

            entity.DGChatLuongHieuQua = request.DGChatLuongHieuQua;
            entity.DGTienDo = request.DGTienDo;
            entity.DGChapHanhCheDoThongTinBaoCao = request.DGChapHanhCheDoThongTinBaoCao;
            entity.DGChapHanhDieuDongLamThemGio = request.DGChapHanhDieuDongLamThemGio;

            entity.UpdatedAt = DateTime.UtcNow;
            _context.PrjCongViec.Update(entity);


            await _context.SaveChangesAsync();

            await LogHoatDong(info, entity, "Đánh giá hoàn thành công việc", string.Empty);

            return new CudResponseDto
            {
                Message = "Đánh giá hoàn thành công việc thành công",
                IsSucceeded = true
            };
        }
    }
}
