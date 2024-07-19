using Microsoft.EntityFrameworkCore;
using Poi.Id.InfraModel.DataAccess;
using Poi.Id.InfraModel.DataAccess.AppPermission;
using Poi.Id.Logic.Dtos;
using Poi.Id.Logic.Interfaces.AppPermission;
using Poi.Id.Logic.Requests.AppPermission;
using Poi.Shared.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Id.Logic.Services.AppPermission
{
    public class PerRoleService : IPerRoleService
    {
        private readonly IdDbContext _context;

        public PerRoleService(IdDbContext context)
        {
            _context = context;
        }

        public async Task<CudResponseDto> AssignFunctionToRole(AssignFunctionToRoleRequest request, TenantInfo info)
        {
            var role = await _context.PerRole
                .Include(x => x.PerRoleFunctionScope)
                .FirstOrDefaultAsync(x => x.Id == request.RoleId);

            if (role == null)
            {
                return new CudResponseDto
                {
                    Id = request.RoleId,
                    Message = "Role not found"
                };
            }

            var functionScope = new List<PerRoleFunctionScope>();

            foreach(var item in request.FunctionScopes) {
                functionScope.Add(new PerRoleFunctionScope
                {
                    PerRoleId = request.RoleId,
                    PerScopeId = item.ScopeId,
                    PerFunctionId = item.FunctionId
                });
            }

            role.PerRoleFunctionScope = functionScope;

            _context.Update(role);

            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = role.Id,
                Message = "Assign function to role successfully"
            };
        }

        public async Task<CudResponseDto> AssignRoleToUser(AssignRoleToUserRequest request, TenantInfo info)
        {
            var user = await _context.Users
                .Include(x => x.PerRoles)
                .FirstOrDefaultAsync(x => x.Id == request.UserId);

            if (user == null)
            {
                return new CudResponseDto
                {
                    Id = request.UserId,
                    Message = "User not found"
                };
            }

            var newRole = await _context.PerRole.FirstOrDefaultAsync(x => x.Id == request.PerRoleId && x.AppCode == info.AppCode);

            var userRole = user.PerRoles.ToList();

            var appRole = user.PerRoles.FirstOrDefault(x => x.AppCode == info.AppCode && x.TenantId == info.TenantId);

            if (appRole != null)
            {
                userRole = user.PerRoles.ToList();
                userRole.Remove(appRole);
            }

            userRole.Add(newRole);

            user.PerRoles = userRole;

            _context.Update(user);

            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = user.Id,
                Message = "Assign role to user successfully"
            };
        }

        public async Task<CudResponseDto> CreateRoleAsync(PerRoleRequest request, TenantInfo info)
        {
            var role = new PerRole
            {
                Name = request.Name,
                Description = request.Description,
                AppCode = info.AppCode,
                TenantId = info.TenantId
            };

            _context.PerRole.Add(role);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = role.Id,
                Message = "Create role successfully"
            };
        }

        public async Task<CudResponseDto> DeleteRoleAsync(Guid id)
        {
            var role = await _context.PerRole.FindAsync(id);
            if (role == null)
            {
                return new CudResponseDto
                {
                    Id = id,
                    Message = "Role not found"
                };
            }

            _context.PerRole.Remove(role);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = id,
                Message = "Delete role successfully"
            };
        }

        public async Task<List<PerRoleFunctionScope>> GetFunctionScopeByRole(Guid roleId, TenantInfo info)
        {
            var data = await _context.PerRoleFunctionScope
                                    .Where(x => x.PerRoleId == roleId)
                                    .ToListAsync();
            return data;
        }

        public async Task<PerRole> GetRoleAsync(Guid id)
        {
            return await _context.PerRole
                .Include(x => x.PerRoleFunctionScope)
                .ThenInclude(f => f.Scope)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<PerRole>> GetRolesAsync(TenantInfo info)
        {
            return await _context.PerRole.Where(x => x.AppCode == info.AppCode && x.TenantId == info.TenantId).ToListAsync();
        }

        public async Task<CudResponseDto> UpdateRoleAsync(Guid id, PerRoleRequest request)
        {
            var role = await _context.PerRole.FindAsync(id);
            if (role == null)
            {
                return new CudResponseDto
                {
                    Id = id,
                    Message = "Role not found"
                };
            }

            role.Name = request.Name;
            role.Description = request.Description;

            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = id,
                Message = "Update role successfully"
            };
        }
    }
}
