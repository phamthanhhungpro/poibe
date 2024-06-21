using Microsoft.EntityFrameworkCore;
using Poi.Id.InfraModel.DataAccess.Prj;
using Poi.Prj.InfraModel.DataAccess;
using Poi.Prj.Logic.Interface;
using Poi.Prj.Logic.Requests;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;

namespace Poi.Prj.Logic.Service
{
    public class ToNhomService : IToNhomService
    {
        private readonly PrjDbContext _context;
        public ToNhomService(PrjDbContext context)
        {
            _context = context;
        }

        public async Task<CudResponseDto> AddAsync(ToNhomRequest ToNhom, TenantInfo info)
        {
            var newToNhom = new PrjToNhom
            {
                TenToNhom = ToNhom.TenToNhom,
                Description = ToNhom.Description,
            };

            newToNhom.ThanhVien = await _context.Users.Where(x => ToNhom.MemberIds.Contains(x.Id)).ToListAsync();
            newToNhom.LanhDao = await _context.Users.Where(x => ToNhom.LanhDaoIds.Contains(x.Id)).ToListAsync();
            newToNhom.TenantId = info.TenantId;

            _context.PrjToNhom.Add(newToNhom);
            await _context.SaveChangesAsync();
            return new CudResponseDto
            {
                Id = newToNhom.Id,
                IsSucceeded = true,
                Message = "Thêm mới thành công"
            };

        }

        public async Task<CudResponseDto> DeleteAsync(Guid id, TenantInfo info)
        {
            var entity = await _context.PrjToNhom.FirstOrDefaultAsync(x => x.Id == id && x.TenantId == info.TenantId);
            if (entity == null)
            {
                return new CudResponseDto
                {
                    Id = id,
                    Message = "Không tìm thấy dữ liệu",
                    IsSucceeded = false
                };
            }

            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow;

            _context.PrjToNhom.Update(entity);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = entity.Id,
                Message = "Xóa thành công",
                IsSucceeded = true
            };
        }

        public async Task<PrjToNhom> GetByIdAsync(Guid id, TenantInfo info)
        {
            return await _context.PrjToNhom
                .Include(x => x.ThanhVien)
                .Include(x => x.LanhDao)
                .FirstOrDefaultAsync(x => x.Id == id && x.TenantId == info.TenantId);
        }

        public async Task<IEnumerable<PrjToNhom>> GetNoPaging(TenantInfo info)
        {
            return await _context.PrjToNhom
                .Include(x => x.ThanhVien)
                .Include(x => x.LanhDao)
                .Where(x => x.TenantId == info.TenantId)
                .ToListAsync();
        }

        public async Task<CudResponseDto> UpdateAsync(Guid id, ToNhomRequest ToNhom, TenantInfo info)
        {
            var entity = await _context.PrjToNhom
                .Include(ToNhom => ToNhom.LanhDao)
                .Include(ToNhom => ToNhom.ThanhVien)
                .FirstOrDefaultAsync(x => x.Id == id && x.TenantId == info.TenantId);
            if (entity == null)
            {
                return new CudResponseDto
                {
                    Id = id,
                    Message = "Không tìm thấy dữ liệu",
                    IsSucceeded = false
                };
            }

            entity.TenToNhom = ToNhom.TenToNhom;
            entity.Description = ToNhom.Description;

            if (ToNhom.LanhDaoIds != null && ToNhom.LanhDaoIds.Any())
            {
                entity.LanhDao = await _context.Users.Where(x => ToNhom.LanhDaoIds.Contains(x.Id)).ToListAsync();
            }

            if (ToNhom.MemberIds != null && ToNhom.MemberIds.Any())
            {
                entity.ThanhVien = await _context.Users.Where(x => ToNhom.MemberIds.Contains(x.Id)).ToListAsync();
            }

            entity.UpdatedAt = DateTime.UtcNow;
            _context.PrjToNhom.Update(entity);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = entity.Id,
                Message = "Cập nhật thành công",
                IsSucceeded = true
            };
        }
    }
}
