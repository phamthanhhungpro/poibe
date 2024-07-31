using Microsoft.EntityFrameworkCore;
using Poi.Id.InfraModel.DataAccess;
using Poi.Id.InfraModel.DataAccess.AppPermission;
using Poi.Id.Logic.Dtos;
using Poi.Id.Logic.Interfaces.AppPermission;
using Poi.Id.Logic.Requests;
using Poi.Id.Logic.Requests.AppPermission;
using Poi.Shared.Model.BaseModel;

namespace Poi.Id.Logic.Services.AppPermission
{
    public class PerApiEndpointService : IPerApiEndpointService
    {
        private readonly IdDbContext _context;

        public PerApiEndpointService(IdDbContext context)
        {
            _context = context;
        }

        public async Task<CudResponseDto> CreateAsync(ApiEndpointRequest request, TenantInfo info)
        {
            var entity = new PerEndpoint
            {
                Name = request.Name,
                Description = request.Description,
                Method = request.Method,
                Path = request.Path,
                IsPublic = request.IsPublic,
                AppCode = info.AppCode
            };

            _context.PerEndpoint.Add(entity);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = entity.Id,
                IsSucceeded = true
            };
        }

        public async Task<CudResponseDto> DeleteAsync(Guid id, TenantInfo info)
        {
            var entity = await _context.PerEndpoint.FindAsync(id);
            if (entity == null)
            {
                return new CudResponseDto
                {
                    IsSucceeded = false
                };
            }

            entity.IsDeleted = true;

            _context.PerEndpoint.Update(entity);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = entity.Id,
                IsSucceeded = true
            };
        }

        public async Task<PagingResponse<PerEndpoint>> GetAllAsync(PagingRequest request, TenantInfo info)
        {
            var pageSize = request.PageSize;
            var pageNumber = request.PageNumber;

            var query = _context.PerEndpoint.Where(x => x.AppCode == info.AppCode).AsNoTracking();

            var data = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var count = await query.CountAsync();

            return new PagingResponse<PerEndpoint>()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Count = count,
                Items = data
            };
        }

        public async Task<PerEndpoint> GetByIdAsync(Guid id, TenantInfo info)
        {
            return await _context.PerEndpoint.FirstOrDefaultAsync(x => x.Id == id && x.AppCode == info.AppCode);
        }

        public async Task<IEnumerable<PerEndpoint>> GetNopaging(TenantInfo info)
        {
            return await _context.PerEndpoint.Where(x => x.AppCode == info.AppCode).OrderBy(o => o.CreatedAt).ToListAsync();
        }

        public async Task<CudResponseDto> UpdateAsync(Guid id, ApiEndpointRequest request, TenantInfo info)
        {
            var entity = await _context.PerEndpoint.FirstOrDefaultAsync(x => x.Id == id && x.AppCode == info.AppCode);
            if (entity == null)
            {
                return new CudResponseDto
                {
                    IsSucceeded = false
                };
            }

            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.Method = request.Method;
            entity.Path = request.Path;
            entity.IsPublic = request.IsPublic;

            _context.PerEndpoint.Update(entity);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = entity.Id,
                IsSucceeded = true
            };
        }
    }
}
