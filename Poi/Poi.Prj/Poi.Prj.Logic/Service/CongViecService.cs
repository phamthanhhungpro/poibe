using Microsoft.EntityFrameworkCore;
using Poi.Id.InfraModel.DataAccess.Prj;
using Poi.Prj.InfraModel.DataAccess;
using Poi.Prj.Logic.Dtos;
using Poi.Prj.Logic.Interface;
using Poi.Prj.Logic.Requests;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;
using Poi.Shared.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                NguoiThucHienId = request.NguoiThucHienId,
                NguoiGiaoViecId = info.UserId,
                CongViecChaId = request.CongViecChaId,
                LoaiCongViecId = request.LoaiCongViecId,
                NhomCongViecId = request.NhomCongViecId,
                TenantId = info.TenantId,
                NguoiPhoiHop = await _context.Users.Where(x => request.NguoiPhoiHopIds.Contains(x.Id)).ToListAsync(),
                TagCongViec = await _context.PrjTagCongViec.Where(x => request.TagCongViecIds.Contains(x.Id)).ToListAsync()
            };

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
            entity.DeletedAt = DateTime.Now;

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
                                            .Where(x => x.DuAnNvChuyenMonId == DuanId && x.TenantId == info.TenantId)
                                            .ToListAsync();
        }

        public async Task<CudResponseDto> UpdateAsync(Guid id, CongViecRequest request, TenantInfo info)
        {
            var entity = await _context.PrjCongViec
                                            .Include(x => x.NguoiPhoiHop)
                                            .Include(x => x.TagCongViec)
                                            .FirstOrDefaultAsync(x => x.Id == id && x.TenantId == info.TenantId);
            if (entity == null)
            {
                return new CudResponseDto
                {
                    Message = "Không tìm thấy dữ liệu",
                    IsSucceeded = false
                };
            }

            entity.TenCongViec = request.TenCongViec;
            entity.MoTa = request.MoTa;
            entity.DuAnNvChuyenMonId = request.DuAnNvChuyenMonId;
            entity.NgayBatDau = request.NgayBatDau.ToUTC();
            entity.NgayKetThuc = request.NgayKetThuc.ToUTC();
            entity.TrangThai = request.TrangThai;
            entity.NguoiThucHienId = request.NguoiThucHienId;
            entity.NguoiGiaoViecId = request.NguoiGiaoViecId;
            entity.CongViecChaId = request.CongViecChaId;
            entity.LoaiCongViecId = request.LoaiCongViecId;
            entity.NhomCongViecId = request.NhomCongViecId;
            entity.NguoiPhoiHop = await _context.Users.Where(x => request.NguoiPhoiHopIds.Contains(x.Id)).ToListAsync();
            entity.TagCongViec = await _context.PrjTagCongViec.Where(x => request.TagCongViecIds.Contains(x.Id)).ToListAsync();

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
            // Lấy danh sách công việc cha (CongViecChaId == null) của dự án theo nhóm công việc
            var listCongViecCha = await _context.PrjCongViec
                                                .Include(x => x.NguoiThucHien)
                                                .Include(x => x.NguoiGiaoViec)
                                                .Include(x => x.NhomCongViec)
                                                .Where(x => x.DuAnNvChuyenMonId == DuanId && x.CongViecChaId == null && x.TenantId == info.TenantId)
                                                .ToListAsync();

            // Group công việc cha theo nhóm công việc
            var result = listCongViecCha.GroupBy(x => x.NhomCongViecId)
                                        .Select(x => new CongViecGroupByNhomCongViecDto
                                        {
                                            NhomCongViecId = x.Key.Value,
                                            TenNhomCongViec = x.FirstOrDefault().NhomCongViec.TenNhomCongViec,
                                            ListCongViec = x.Select(y => new CongViecGridDto
                                            {
                                                Id = y.Id,
                                                TenCongViec = y.TenCongViec,
                                                MoTa = y.MoTa,
                                                NgayBatDau = y.NgayBatDau.ToLocalTime(),
                                                NgayKetThuc = y.NgayKetThuc.ToLocalTime(),
                                                TrangThai = y.TrangThai,
                                                NguoiThucHien = y.NguoiThucHien,
                                                NguoiGiaoViec = y.NguoiGiaoViec,
                                                CreatedAt = y.CreatedAt.ToLocalTime().ToString("dd/MM/yyyy HH:mm")
                                            }).ToList()
                                        });

            return result;
        }

        public async Task<IEnumerable<CongViecKanbanDto>> GetCongViecKanban(TenantInfo info, Guid DuanId)
        {
            // Lấy danh sách cột kanban của dự án
            var listKanban = await _context.PrjKanban
                                            .Where(x => x.DuAnNvChuyenMonId == DuanId)
                                            .OrderBy(x => x.ThuTu)
                                            .ToListAsync();

            // Lấy danh sách công việc của dự án
            var listCongViec = await _context.PrjCongViec
                                            .Include(x => x.NguoiThucHien)
                                            .Include(x => x.NguoiGiaoViec)
                                            .Include(x => x.NhomCongViec)
                                            .Include(x => x.TagCongViec)
                                            .Where(x => x.DuAnNvChuyenMonId == DuanId)
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
                    NguoiThucHien = y.NguoiThucHien,
                    NguoiGiaoViec = y.NguoiGiaoViec,
                    TagCongViec = y.TagCongViec,
                    CreatedAt = y.CreatedAt.ToLocalTime().ToString("dd/MM/yyyy HH:mm")
                }).ToList()
            });

            return result;
        }

        public async Task<CudResponseDto> UpdateKanbanStatus(TenantInfo info, UpdateCongViecKanbanStatusRequest request)
        {
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

            // Update trang thai cong viec
            congViec.TrangThai = kanban.TrangThaiCongViec;
            _context.PrjCongViec.Update(congViec);

            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                IsSucceeded = true,
                Message = "Cập nhật trạng thái công việc thành công"
            };

        }
    }
}
