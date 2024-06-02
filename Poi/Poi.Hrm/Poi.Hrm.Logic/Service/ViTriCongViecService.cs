using Microsoft.EntityFrameworkCore;
using Poi.Hrm.Logic.Interface;
using Poi.Id.InfraModel.DataAccess;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;

namespace Poi.Hrm.Logic.Service
{
    public class ViTriCongViecService : IViTriCongViecService
    {
        private readonly HrmDbContext _context;

        public ViTriCongViecService(HrmDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HrmViTriCongViec>> GetNoPaging(TenantInfo info)
        {
            return await _context.HrmViTriCongViec
                .Include(x => x.Tenant)
                .Where(x => x.Tenant.Id == info.TenantId)
                .ToListAsync();
        }

        public async Task<HrmViTriCongViec> GetByIdAsync(Guid id, TenantInfo info)
        {
            return await _context.HrmViTriCongViec
                .Include(x => x.Tenant)
                .Where(x => x.Tenant.Id == info.TenantId)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<CudResponseDto> AddAsync(HrmViTriCongViec viTriCongViec, TenantInfo info)
        {
            var model = new HrmViTriCongViec
            {
                TenViTri = viTriCongViec.TenViTri,
                MoTa = viTriCongViec.MoTa,
                Tenant = await _context.Tenants.FindAsync(info.TenantId)
            };

            _context.HrmViTriCongViec.Add(model);

            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                IsSucceeded = true,
                Id = viTriCongViec.Id
            };
        }

        public async Task<CudResponseDto> UpdateAsync(Guid id, HrmViTriCongViec viTriCongViec, TenantInfo info)
        {
            var viTriCongViecInDb = await _context.HrmViTriCongViec
                .Include(Id => Id.Tenant)
                .Where(x => x.Tenant.Id == info.TenantId)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (viTriCongViecInDb == null)
            {
                return new CudResponseDto
                {
                    IsSucceeded = false,
                };
            }

            viTriCongViecInDb.TenViTri = viTriCongViec.TenViTri;
            viTriCongViecInDb.MoTa = viTriCongViec.MoTa;

            viTriCongViecInDb.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                IsSucceeded = true,
                Id = viTriCongViecInDb.Id
            };

        }

        public async Task<CudResponseDto> DeleteAsync(Guid id, TenantInfo info)
        {
            var viTriCongViec = await _context.HrmViTriCongViec
                .Include(x => x.Tenant)
                .Where(x => x.Tenant.Id == info.TenantId)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (viTriCongViec == null)
            {
                return new CudResponseDto
                {
                    IsSucceeded = false,
                };
            }

            viTriCongViec.DeletedAt = DateTime.UtcNow;
            viTriCongViec.IsDeleted = true;

            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                IsSucceeded = true,
                Id = id
            };
        }
    }
}
