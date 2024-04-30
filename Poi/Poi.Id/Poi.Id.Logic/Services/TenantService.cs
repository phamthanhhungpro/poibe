using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Poi.Id.InfraModel.DataAccess;
using Poi.Id.Logic.Dtos;
using Poi.Id.Logic.Interfaces;
using Poi.Id.Logic.Requests;
using Poi.Shared.Model.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            // generate a new tenant for me
            var newTenant = new Tenant
            {
                Name = tenant.Name,
                Description = tenant.Description,
                Code = tenant.Code
            };

            // add the tenant to the database
            _context.Tenants.Add(newTenant);
            await _context.SaveChangesAsync();

            // return the new tenant
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

        public async Task<PagingResponse<Tenant>> GetTenant(PagingRequest request)
        {
            var pageSize = request.PageSize;
            var pageNumber = request.PageNumber;

            var query = _context.Tenants.OrderByDescending(o => o.CreatedAt).AsNoTracking();

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
            // find the tenant
            var toUpdateTenant = await _context.Tenants.FindAsync(id);

            if (tenant == null)
            {
                throw new Exception($"Tenant {Error.NotFound}");
            }

            _mapper.Map(tenant, toUpdateTenant);

            toUpdateTenant.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = id
            };
        }
    }
}
