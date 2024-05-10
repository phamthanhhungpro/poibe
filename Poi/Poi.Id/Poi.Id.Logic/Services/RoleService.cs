using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Poi.Id.InfraModel.DataAccess;
using Poi.Id.Logic.Dtos;
using Poi.Id.Logic.Interfaces;
using Poi.Id.Logic.Requests;
using Poi.Shared.Model.Constants;

namespace Poi.Id.Logic.Services
{
    public class RoleService : IRoleService
    {
        private readonly IdDbContext _context;
        private readonly IMapper _mapper;

        public RoleService(IdDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CudResponseDto> CreateRole(RoleRequest role)
        {
            var newRole = new Role()
            {
                Code = role.Code,
                Name = role.Name,
                Description = role.Description,
            };

            newRole.CreatedAt = DateTime.UtcNow;

            _context.Roles.Add(newRole);
            await _context.SaveChangesAsync();

            return new CudResponseDto()
            {
                Id = newRole.Id,
            };
        }

        public async Task<CudResponseDto> DeleteRole(Guid id)
        {
            // find the role
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
            {
                throw new Exception($"role {Error.NotFound}");
            }
            role.DeletedAt = DateTime.UtcNow;
            role.IsDeleted = true;

            await _context.SaveChangesAsync();

            // return the deleted role
            return new CudResponseDto
            {
                Id = role.Id
            };
        }

        public async Task<PagingResponse<Role>> GetRole(PagingRequest request)
        {
            var pageSize = request.PageSize;
            var pageNumber = request.PageNumber;

            var query = _context.Roles
                .Where(r => r.Code != RoleConstants.ROLE_SSA)
                .OrderByDescending(o => o.CreatedAt).AsNoTracking();

            var data = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var count = await query.CountAsync();

            return new PagingResponse<Role>()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Count = count,
                Items = data
            };
        }

        public async Task<List<Role>> GetNoPaging()
        {
            return await _context.Roles
                .Where(r => r.Code != RoleConstants.ROLE_SSA)
                .OrderByDescending(o => o.CreatedAt)
                .AsNoTracking().ToListAsync();
        }

        public async Task<Role> GetRoleById(Guid id)
        {
            return await _context.Roles.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<CudResponseDto> UpdateRole(Guid id, RoleRequest role)
        {
            // find the role
            var toUpdateRole = await _context.Roles.FindAsync(id);

            if (toUpdateRole == null)
            {
                throw new Exception($"role {Error.NotFound}");
            }

            _mapper.Map(role, toUpdateRole);

            toUpdateRole.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = id
            };
        }
    }
}
