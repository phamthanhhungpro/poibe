using Microsoft.EntityFrameworkCore;
using Poi.Hrm.Logic.Interface;
using Poi.Id.InfraModel.DataAccess;
using Poi.Id.InfraModel.DataAccess.Hrm;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;

namespace Poi.Hrm.Logic.Service
{
    public class VaiTroService : IVaiTroService
    {
        private readonly HrmDbContext _context;

        public VaiTroService(HrmDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HrmVaiTro>> GetNoPaging(TenantInfo info)
        {
            return await _context.HrmVaiTro
                .Include(x => x.Tenant)
                .Where(x => x.Tenant.Id == info.TenantId)
                .ToListAsync();
        }

        public async Task<HrmVaiTro> GetByIdAsync(Guid id, TenantInfo info)
        {
            return await _context.HrmVaiTro
                .Include(x => x.Tenant)
                .Include(x => x.HrmNhomChucNang)
                .Where(x => x.Tenant.Id == info.TenantId)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<CudResponseDto> AddAsync(HrmVaiTro vaiTro, TenantInfo info)
        {
            var model = new HrmVaiTro
            {
                TenVaiTro = vaiTro.TenVaiTro,
                MoTa = vaiTro.MoTa,
                Tenant = await _context.Tenants.FindAsync(info.TenantId)
            };

            _context.HrmVaiTro.Add(model);

            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                IsSucceeded = true,
                Id = vaiTro.Id
            };
        }

        public async Task<CudResponseDto> UpdateAsync(Guid id, HrmVaiTro vaiTro, TenantInfo info)
        {
            var vaiTroInDb = await _context.HrmVaiTro
                .Include(Id => Id.Tenant)
                .Where(x => x.Tenant.Id == info.TenantId)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (vaiTroInDb == null)
            {
                return new CudResponseDto
                {
                    IsSucceeded = false,
                };
            }

            vaiTroInDb.TenVaiTro = vaiTro.TenVaiTro;
            vaiTroInDb.MoTa = vaiTro.MoTa;

            vaiTroInDb.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                IsSucceeded = true,
                Id = vaiTroInDb.Id
            };

        }

        public async Task<CudResponseDto> DeleteAsync(Guid id, TenantInfo info)
        {
            var vaiTro = await _context.HrmVaiTro
                .Include(x => x.Tenant)
                .Where(x => x.Tenant.Id == info.TenantId)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (vaiTro == null)
            {
                return new CudResponseDto
                {
                    IsSucceeded = false,
                };
            }

            vaiTro.DeletedAt = DateTime.UtcNow;
            vaiTro.IsDeleted = true;

            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                IsSucceeded = true,
                Id = id
            };
        }
    }
}
