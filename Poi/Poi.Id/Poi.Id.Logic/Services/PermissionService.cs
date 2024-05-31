using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Poi.Id.InfraModel.DataAccess;
using Poi.Id.Logic.Dtos;
using Poi.Id.Logic.Interfaces;
using Poi.Id.Logic.Requests;
using Poi.Shared.Model.BaseModel;

namespace Poi.Id.Logic.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IdDbContext _context;
        private readonly IMapper _mapper;
        public PermissionService(IdDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<CudResponseDto> AssignPermission(AssignPermissionRequest request)
        {
            var role = await _context.Roles
                                            .Include(r => r.Permissions)
                                            .FirstOrDefaultAsync(r => r.Id == request.RoleId);
            if (role == null)
            {
                return new CudResponseDto { IsSucceeded = false };
            }

            var permissions = await _context.Permissions.Where(p => request.PermissionIds.Contains(p.Id)).ToListAsync();
            if (permissions.Count == 0)
            {
                return new CudResponseDto { IsSucceeded = false };
            }
            role.Permissions = permissions;
            role.UpdatedAt = DateTime.UtcNow;

            _context.Roles.Update(role);

            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                IsSucceeded = true,
                Id = role.Id
            };
        }

        public async Task<CudResponseDto> CreatePermission(CreatePermissionRequest request)
        {
            var permission = new Permission()
            {
                Name = request.Name,
                Description = request.Description,
                Method = request.Method,
                Path = request.Path,
            };

            permission.CreatedAt = DateTime.UtcNow;

            _context.Permissions.Add(permission);
            await _context.SaveChangesAsync();

            return new CudResponseDto()
            {
                Id = permission.Id,
            };
        }

        public async Task<CudResponseDto> DeletePermission(Guid id)
        {
            var permission = await _context.Permissions.FindAsync(id);

            if (permission == null)
            {
                return new CudResponseDto { IsSucceeded = false };
            }

            permission.DeletedAt = DateTime.UtcNow;
            permission.IsDeleted = true;

            _context.Permissions.Update(permission);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = id,
                IsSucceeded = true
            };
        }

        public async Task<IList<Permission>> GetNoPaging()
        {
            return await _context.Permissions.ToListAsync();
        }

        public async Task<PagingResponse<Permission>> GetPermission(PagingRequest request, TenantInfo info)
        {
            var pageSize = request.PageSize;
            var pageNumber = request.PageNumber;

            var query = _context.Permissions
                .OrderByDescending(o => o.CreatedAt).AsNoTracking();

            var total = query.Count();
            var data = await query.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();

            return new PagingResponse<Permission>
            {
                Items = data,
                Count = total,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<CudResponseDto> UpdatePermission(Guid id, UpdatePermissionRequest request)
        {
            var permission = await _context.Permissions.FindAsync(id);

            if (permission == null)
            {
                return new CudResponseDto { IsSucceeded = false };
            }

            permission.Name = request.Name;
            permission.Description = request.Description;
            permission.Method = request.Method;
            permission.Path = request.Path;

            permission.UpdatedAt = DateTime.UtcNow;

            _context.Permissions.Update(permission);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = id,
                IsSucceeded = true
            };
        }
    }
}
