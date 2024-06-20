using Microsoft.EntityFrameworkCore;
using Poi.Id.InfraModel.DataAccess.Prj;
using Poi.Prj.InfraModel.DataAccess;
using Poi.Prj.Logic.Interface;
using Poi.Prj.Logic.Requests;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;

namespace Poi.Prj.Logic.Service
{
    public class LinhVucService : ILinhVucService
    {
        private readonly PrjDbContext _context;

        public LinhVucService(PrjDbContext context)
        {
            _context = context;
        }

        public async Task<CudResponseDto> AddAsync(LinhVucRequest LinhVuc, TenantInfo info)
        {
            var entity = new PrjLinhVuc
            {
                TenLinhVuc = LinhVuc.TenLinhVuc,
                Description = LinhVuc.Description,
                TenantId = info.TenantId
            };

            _context.PrjLinhVuc.Add(entity);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = entity.Id,
                Message = "Thêm mới thành công",
                IsSucceeded = true
            };
        }

        public async Task<CudResponseDto> DeleteAsync(Guid id, TenantInfo info)
        {
            var entity = await _context.PrjLinhVuc.FirstOrDefaultAsync(x => x.Id == id && x.TenantId == info.TenantId);
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

            _context.PrjLinhVuc.Update(entity);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = id,
                Message = "Xóa thành công",
                IsSucceeded = true
            };
        }

        public async Task<PrjLinhVuc> GetByIdAsync(Guid id, TenantInfo info)
        {
            return await _context.PrjLinhVuc.FirstOrDefaultAsync(x => x.Id == id && x.TenantId == info.TenantId);
        }

        public async Task<IEnumerable<PrjLinhVuc>> GetNoPaging(TenantInfo info)
        {
            return await _context.PrjLinhVuc.Where(x => x.TenantId == info.TenantId).ToListAsync();
        }

        public async Task<CudResponseDto> UpdateAsync(Guid id, LinhVucRequest LinhVuc, TenantInfo info)
        {
            var entity = await _context.PrjLinhVuc.FirstOrDefaultAsync(x => x.Id == id && x.TenantId == info.TenantId);
            if (entity == null)
            {
                return new CudResponseDto
                {
                    Id = id,
                    Message = "Không tìm thấy dữ liệu",
                    IsSucceeded = false
                };
            }

            entity.TenLinhVuc = LinhVuc.TenLinhVuc;
            entity.Description = LinhVuc.Description;

            _context.PrjLinhVuc.Update(entity);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = id,
                Message = "Cập nhật thành công",
                IsSucceeded = true
            };
        }
    }
}
