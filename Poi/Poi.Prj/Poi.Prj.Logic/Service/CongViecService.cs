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
using System.Linq.Expressions;
using System.Text.Json;

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

            if (info.UserId != duAn.QuanLyDuAnId)
            {
                entity.TrangThaiChiTiet = TrangThaiCongViecChiTiet.ChoDuyetDeXuat;
            }

            _context.PrjCongViec.Add(entity);

            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = entity.Id,
                Message = "Thêm thành công",
                IsSucceeded = true
            };
        }

        public async Task<CudResponseDto> DeleteAsync(Guid id, TenantInfo info)
        {
            var entity = await _context.PrjCongViec.FindAsync(id);
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

            if (!string.IsNullOrEmpty(request.TrangThai))
            {
                // get settings
                var settings = await _context.PrjDuAnSetting.FirstOrDefaultAsync(x => x.DuAnNvChuyenMonId == entity.DuAnNvChuyenMonId && x.Key == TrangThaiCongViecHelper.TrangThaiSettingKey);
                if (settings != null)
                {
                    var trangThaiSetting = JsonSerializer.Deserialize<List<TrangThaiCongViecSettingDto>>(settings.JsonValue);

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

            entity.NgayKetThuc = request.NgayKetThuc.ToUTC();
            entity.NguoiDuocGiaoId = request.NguoiDuocGiaoId;
            entity.NguoiPhoiHop = await _context.Users.Where(x => request.NguoiPhoiHopIds.Contains(x.Id)).ToListAsync();
            entity.NguoiThucHien = await _context.Users.Where(x => request.NguoiThucHienIds.Contains(x.Id)).ToListAsync();

            _context.PrjCongViec.Update(entity);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Message = "Cập nhật thành công",
                IsSucceeded = true
            };
        }

        public async Task<IEnumerable<CongViecGroupByNhomCongViecDto>> GetCongViecGrid(TenantInfo info, Guid DuanId)
        {
            Expression<Func<PrjCongViec, bool>> filterExpression = x => true;
            // check scope
            if (info.IsNeedCheckScope && !string.IsNullOrEmpty(info.RequestScopeCode))
            {
                // check scope
                var user = await _context.Users
                                        .Include(x => x.LanhDaoPhongBan)
                                        .Include(x => x.ThanhVienPhongBan)
                                        .Include(x => x.LanhDaoToNhom)
                                        .Include(x => x.ThanhVienToNhom)
                                        .FirstOrDefaultAsync(x => x.Id == info.UserId);

                switch (info.RequestScopeCode)
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

            // Lấy danh sách công việc cha (CongViecChaId == null) của dự án theo nhóm công việc
            var listCongViecCha = await _context.PrjCongViec
                                                .Include(x => x.NguoiThucHien)
                                                .Include(x => x.NguoiGiaoViec)
                                                .Include(x => x.NhomCongViec)
                                                .Include(x => x.NguoiDuocGiao)
                                                .Where(x => x.DuAnNvChuyenMonId == DuanId && x.CongViecChaId == null && x.TenantId == info.TenantId)
                                                .Where(filterExpression)
                                                .ToListAsync();

            // Group công việc cha theo nhóm công việc
            var result = listCongViecCha.GroupBy(x => x.NhomCongViecId)
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
                                            }).ToList()
                                        });

            return result;
        }

        public async Task<IEnumerable<CongViecKanbanDto>> GetCongViecKanban(TenantInfo info, Guid DuanId)
        {
            Expression<Func<PrjCongViec, bool>> filterExpression = x => true;
            // check scope
            if (info.IsNeedCheckScope && !string.IsNullOrEmpty(info.RequestScopeCode))
            {
                // check scope
                var user = await _context.Users
                                        .Include(x => x.LanhDaoPhongBan)
                                        .Include(x => x.ThanhVienPhongBan)
                                        .Include(x => x.LanhDaoToNhom)
                                        .Include(x => x.ThanhVienToNhom)
                                        .FirstOrDefaultAsync(x => x.Id == info.UserId);

                switch (info.RequestScopeCode)
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

            if (!string.IsNullOrEmpty(kanban.TrangThaiCongViec))
            {
                // get settings
                var settings = await _context.PrjDuAnSetting.FirstOrDefaultAsync(x => x.DuAnNvChuyenMonId == congViec.DuAnNvChuyenMonId
                                                                                   && x.Key == TrangThaiCongViecHelper.TrangThaiSettingKey);
                if (settings != null)
                {
                    var trangThaiSetting = JsonSerializer.Deserialize<List<TrangThaiCongViecSettingDto>>(settings.JsonValue);

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

            entity.NgayGiaHan = request.NgayKetThuc.ToUTC();
            entity.TrangThaiChiTiet = TrangThaiCongViecChiTiet.ChoDuyetGiaHan;

            _context.PrjCongViec.Update(entity);
            await _context.SaveChangesAsync();

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
            // Get du an
            var duAn = await _context.PrjDuAnNvChuyenMon.FindAsync(request.DuanId);

            if (info.UserId != duAn.QuanLyDuAnId)
            {
                return new CudResponseDto
                {
                    IsSucceeded = false,
                    Message = "Bạn không có quyền duyệt gia hạn công việc"
                };
            }

            // Get cong viec
            var result = await _context.PrjCongViec
                .Where(x => request.CongViecIds.Contains(x.Id))
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
                return new CudResponseDto
                {
                    IsSucceeded = true,
                    Message = "Duyệt gia hạn công việc thành công"
                };
            }
        }

        public async Task<CudResponseDto> ApproveTrangThaiCongViec(TenantInfo info, ApproveTrangThaiCongViec request)
        {
            // Get du an
            var duAn = await _context.PrjDuAnNvChuyenMon.FindAsync(request.DuanId);

            if (info.UserId != duAn.QuanLyDuAnId)
            {
                return new CudResponseDto
                {
                    IsSucceeded = false,
                    Message = "Bạn không có quyền duyệt thay đổi trạng thái công việc"
                };
            }

            var result = await _context.PrjCongViec
                .Where(x => request.CongViecIds.Contains(x.Id))
                .ExecuteUpdateAsync(x => x.SetProperty(p => p.TrangThai, p => p.TrangThai)
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
                return new CudResponseDto
                {
                    IsSucceeded = true,
                    Message = "Duyệt gia hạn công việc thành công"
                };
            }
        }

        public async Task<CudResponseDto> ApproveDeXuatCongViec(TenantInfo info, ApproveDeXuatCongViec request)
        {
            var duAn = await _context.PrjDuAnNvChuyenMon.FindAsync(request.DuanId);

            if (info.UserId != duAn.QuanLyDuAnId)
            {
                return new CudResponseDto
                {
                    IsSucceeded = false,
                    Message = "Bạn không có quyền duyệt đề xuất công việc"
                };
            }

            // Get cong viec
            var result = await _context.PrjCongViec
                .Where(x => request.CongViecIds.Contains(x.Id))
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
                return new CudResponseDto
                {
                    IsSucceeded = true,
                    Message = "Duyệt đề xuất công việc thành công"
                };
            }
        }

        public async Task<CudResponseDto> RejectDeXuatCongViec(TenantInfo info, RejectCongViecRequest request)
        {
            var duAn = await _context.PrjDuAnNvChuyenMon.FindAsync(request.DuanId);

            if (info.UserId != duAn.QuanLyDuAnId)
            {
                return new CudResponseDto
                {
                    IsSucceeded = false,
                    Message = "Bạn không có quyền từ chối đề xuất công việc"
                };
            }

            // Get cong viec
            var result = await _context.PrjCongViec
                .Where(x => request.CongViecIds.Contains(x.Id))
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
                return new CudResponseDto
                {
                    IsSucceeded = true,
                    Message = "Từ chối đề xuất công việc thành công"
                };
            }

        }

        public async Task<CudResponseDto> RejectGiaHanCongViec(TenantInfo info, RejectCongViecRequest request)
        {
            var duAn = await _context.PrjDuAnNvChuyenMon.FindAsync(request.DuanId);

            if (info.UserId != duAn.QuanLyDuAnId)
            {
                return new CudResponseDto
                {
                    IsSucceeded = false,
                    Message = "Bạn không có quyền từ chối gia hạn công việc"
                };
            }

            // Get cong viec
            var result = await _context.PrjCongViec
                .Where(x => request.CongViecIds.Contains(x.Id))
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
                return new CudResponseDto
                {
                    IsSucceeded = true,
                    Message = "Từ chối gia hạn công việc thành công"
                };
            }
        }

        public async Task<CudResponseDto> RejectTrangThaiCongViec(TenantInfo info, RejectCongViecRequest request)
        {
            var duAn = await _context.PrjDuAnNvChuyenMon.FindAsync(request.DuanId);

            if (info.UserId != duAn.QuanLyDuAnId)
            {
                return new CudResponseDto
                {
                    IsSucceeded = false,
                    Message = "Bạn không có quyền từ chối thay đổi trạng thái công việc"
                };
            }

            // Get cong viec
            var result = await _context.PrjCongViec
                .Where(x => request.CongViecIds.Contains(x.Id))
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
                return new CudResponseDto
                {
                    IsSucceeded = true,
                    Message = "Từ chối thay đổi trạng thái công việc thành công"
                };
            }
        }
    }
}
