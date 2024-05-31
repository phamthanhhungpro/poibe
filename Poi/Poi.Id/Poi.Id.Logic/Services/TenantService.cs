using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Poi.Id.InfraModel.DataAccess;
using Poi.Id.Logic.Dtos;
using Poi.Id.Logic.Interfaces;
using Poi.Id.Logic.Requests;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Constants;

namespace Poi.Id.Logic.Services
{
    public class TenantService : ITenantService
    {
        private readonly IdDbContext _context;
        private readonly IMapper _mapper;

        public TenantService(IdDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CudResponseDto> CreateTenant(CreateTenantRequest tenant)
        {
            var apps = _context.Apps.Where(x => tenant.AppIds.Contains(x.Id)).ToList();

            var newTenant = new Tenant
            {
                Name = tenant.Name,
                Description = tenant.Description,
                Code = tenant.Code,
                Apps = apps
            };

            newTenant.CreatedAt = DateTime.UtcNow;

            _context.Tenants.Add(newTenant);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = newTenant.Id
            };
        }

        public async Task<CudResponseDto> DeleteTenant(Guid id)
        {
            // find the tenant
            var tenant = await _context.Tenants.FindAsync(id);

            if (tenant == null)
            {
                throw new Exception($"Tenant {Error.NotFound}");
            }
            tenant.DeletedAt = DateTime.UtcNow;
            tenant.IsDeleted = true;

            await _context.SaveChangesAsync();

            // return the deleted tenant
            return new CudResponseDto
            {
                Id = tenant.Id
            };
        }

        public async Task<IList<Tenant>> GetTenantByInfo(TenantInfo request)
        {
            if (request.Role == RoleConstants.ROLE_SSA)
            {
                return await _context.Tenants.Include(t => t.Apps).ToListAsync();
            }
            else
            {
                return await _context.Tenants.Where(a => a.Id == request.TenantId).ToListAsync();
            }
        }

        public async Task<PagingResponse<Tenant>> GetTenant(PagingRequest request)
        {
            var pageSize = request.PageSize;
            var pageNumber = request.PageNumber;

            var query = _context.Tenants
                .Include(t => t.Apps)
                .OrderByDescending(o => o.CreatedAt).AsNoTracking();

            var data = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var count = await query.CountAsync();

            return new PagingResponse<Tenant>()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Count = count,
                Items = data
            };
        }

        public async Task<Tenant> GetTenantById(Guid id)
        {
            return await _context.Tenants.Include(t => t.Apps).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<CudResponseDto> UpdateTenant(Guid id, CreateTenantRequest tenant)
        {
            // find apps
            var apps = _context.Apps.Where(x => tenant.AppIds.Contains(x.Id)).ToList();

            // find the tenant
            var toUpdateTenant = await _context.Tenants.Include(t => t.Apps).FirstOrDefaultAsync(t => t.Id == id);

            if (toUpdateTenant == null)
            {
                throw new Exception($"Tenant {Error.NotFound}");
            }

            _mapper.Map(tenant, toUpdateTenant);

            toUpdateTenant.Apps = apps;

            toUpdateTenant.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = id
            };
        }
    }
}
