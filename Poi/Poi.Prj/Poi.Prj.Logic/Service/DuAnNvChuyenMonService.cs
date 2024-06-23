using Microsoft.EntityFrameworkCore;
using Poi.Id.InfraModel.DataAccess.Prj;
using Poi.Prj.InfraModel.DataAccess;
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
    public class DuAnNvChuyenMonService : IDuAnNvChuyenMonService
    {
        private readonly PrjDbContext _context;

        public DuAnNvChuyenMonService(PrjDbContext context)
        {
            _context = context;
        }

        public async Task<CudResponseDto> AddAsync(DuAnNvChuyenMonRequest request, TenantInfo info)
        {
            var entity = new PrjDuAnNvChuyenMon
            {
                TenDuAn = request.TenDuAn,
                MoTaDuAn = request.MoTaDuAn,
                ToNhomId = request.ToNhomId,
                PhongBanBoPhanId = request.PhongBanBoPhanId,
                QuanLyDuAnId = request.QuanLyDuAnId,
                ThoiGianBatDau = request.ThoiGianBatDau.ToUTC(),
                ThoiGianKetThuc = request.ThoiGianKetThuc.ToUTC(),
                LinhVucId = request.LinhVucId,
                IsNhiemVuChuyenMon = request.IsNhiemVuChuyenMon,
                TenantId = info.TenantId
            };
            entity.CreatedBy = info.UserId;
            entity.ThanhVienDuAn = _context.Users.Where(x => request.ThanhVienDuAnIds.Contains(x.Id)).ToList();

            _context.PrjDuAnNvChuyenMon.Add(entity);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = entity.Id,
                Message = "Success",
                IsSucceeded = true
            };
        }

        public async Task<CudResponseDto> DeleteAsync(Guid id, TenantInfo info)
        {
            var entity = await _context.PrjDuAnNvChuyenMon.FindAsync(id);
            if (entity == null)
            {
                return new CudResponseDto
                {
                    Id = id,
                    Message = "Not found",
                    IsSucceeded = false
                };
            }

            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow;

            _context.PrjDuAnNvChuyenMon.Update(entity);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = id,
                Message = "Success",
                IsSucceeded = true
            };
        }

        public async Task<PrjDuAnNvChuyenMon> GetByIdAsync(Guid id, TenantInfo info)
        {
            return await _context.PrjDuAnNvChuyenMon
                .Include(x => x.ThanhVienDuAn)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<PrjDuAnNvChuyenMon>> GetNoPaging(bool isNvChuyenMon, TenantInfo info)
        {
            return await _context.PrjDuAnNvChuyenMon
                                .Include(x => x.ThanhVienDuAn)
                                .Include(x => x.QuanLyDuAn)
                                .Include(x => x.LinhVuc)
                                .Include(x => x.ToNhom).ThenInclude(x => x.LanhDao)
                                .Include(x => x.ToNhom).ThenInclude(x => x.ThanhVien)

                                .Include(x => x.PhongBanBoPhan).ThenInclude(x => x.QuanLy)
                                .Where(x => x.IsNhiemVuChuyenMon == isNvChuyenMon && x.TenantId == info.TenantId)
                                .ToListAsync();
        }

        public async Task<CudResponseDto> UpdateAsync(Guid id, DuAnNvChuyenMonRequest request, TenantInfo info)
        {
            var entity = await _context.PrjDuAnNvChuyenMon
                                        .Include(x => x.ThanhVienDuAn)
                                        .FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                return new CudResponseDto
                {
                    Id = id,
                    Message = "Not found",
                    IsSucceeded = false
                };
            }

            entity.TenDuAn = request.TenDuAn;
            entity.MoTaDuAn = request.MoTaDuAn;
            entity.ToNhomId = request.ToNhomId;
            entity.PhongBanBoPhanId = request.PhongBanBoPhanId;
            entity.QuanLyDuAnId = request.QuanLyDuAnId;
            entity.ThoiGianBatDau = request.ThoiGianBatDau;
            entity.ThoiGianKetThuc = request.ThoiGianKetThuc;
            entity.LinhVucId = request.LinhVucId;
            entity.IsNhiemVuChuyenMon = request.IsNhiemVuChuyenMon;

            entity.ThanhVienDuAn = _context.Users.Where(x => request.ThanhVienDuAnIds.Contains(x.Id)).ToList();

            _context.PrjDuAnNvChuyenMon.Update(entity);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = id,
                Message = "Success",
                IsSucceeded = true
            };
        }
    }
}
