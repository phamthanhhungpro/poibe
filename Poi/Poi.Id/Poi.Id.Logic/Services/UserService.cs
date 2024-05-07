using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Poi.Id.InfraModel.DataAccess;
using Poi.Id.Logic.Dtos;
using Poi.Id.Logic.Interfaces;
using Poi.Id.Logic.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Id.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly IdDbContext _context;
        private readonly IMapper _mapper;

        public UserService(IdDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CudResponseDto> DeleteUser(Guid id)
        {
            var toDeleteUser = await _context.Users.FindAsync(id);

            if (toDeleteUser == null)
            {
                return new CudResponseDto { IsSucceeded = false };
            }

            toDeleteUser.DeletedAt = DateTime.UtcNow;
            toDeleteUser.IsDeleted = true;

            _context.Users.Update(toDeleteUser);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = id,
                IsSucceeded = true
            };
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await _context.Users
                .Include(t => t.Apps)
                .Include(t => t.Tenant)
                .Include(t => t.Role)
                .Include(t => t.Group)
                .Include(t => t.Managers)
                .Include(t => t.DirectReports)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<PagingResponse<UserListInfoDto>> GetUsers(PagingRequest request)
        {
            var pageSize = request.PageSize;
            var pageNumber = request.PageNumber;

            var query = _context.Users
                .Include(t => t.Apps)
                .Include(t => t.Tenant)
                .Include(t => t.Role)
                .Include(t => t.Group)
                .OrderByDescending(o => o.CreatedAt).AsNoTracking();

            var data = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize)
                .Select(x => new UserListInfoDto
                {
                    Id = x.Id,
                    SurName = x.SurName,
                    Name = x.Name,
                    Email = x.Email,
                    UserName = x.UserName,
                    Avatar = x.Avatar,
                    Phone = x.Phone,
                    Role = x.Role.Name,
                    GroupName = x.Group.Name,
                    TenantName = x.Tenant.Name,
                    IsActive = x.IsActive
                }).ToListAsync();

            var count = await query.CountAsync();

            return new PagingResponse<UserListInfoDto>()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Count = count,
                Items = data
            };
        }

        public async Task<CudResponseDto> UpdateUser(Guid id, UpdateUserRequest request)
        {
            var toUpdateUser = await _context.Users
                                            .Include(u => u.Group)
                                            .Include(u => u.Apps)
                                            .Include(u => u.Role)
                                            .Include(u => u.Tenant)
                                            .Include(u => u.Managers)
                                            .Include(u => u.DirectReports)
                                            .FirstOrDefaultAsync(u => u.Id == id);

            if (toUpdateUser == null)
            {
                   return new CudResponseDto { IsSucceeded = false };
            }

            _mapper.Map(request, toUpdateUser);

            // update roles
            if (request.RoleId != null)
            {
                var role = await _context.Roles.FindAsync(request.RoleId);
                if (role == null)
                {
                    return new CudResponseDto { IsSucceeded = false };
                }
                toUpdateUser.Role = role;
            }

            // update group
            if (request.GroupId != null)
            {
                var group = await _context.Groups.FindAsync(request.GroupId);
                if (group == null)
                {
                    return new CudResponseDto { IsSucceeded = false };
                }
                toUpdateUser.Group = group;

                // update Tenant by Tenant of group
                toUpdateUser.Tenant = group.Tenant;
            }

            // update apps
            if (request.AppIds != null)
            {
                var apps = await _context.Apps.Where(t => request.AppIds.Contains(t.Id)).ToListAsync();
                if (apps == null)
                {
                    return new CudResponseDto { IsSucceeded = false };
                }
                toUpdateUser.Apps = apps;
            }

            // update list manager
            if (request.ManagerIds != null)
            {
                var managers = await _context.Users.Where(t => request.ManagerIds.Contains(t.Id)).ToListAsync();
                if (managers == null)
                {
                    return new CudResponseDto { IsSucceeded = false };
                }
                toUpdateUser.Managers = managers;
            }

            // update list direct report
            if (request.DirectReportIds != null)
            {
                var directReports = await _context.Users.Where(t => request.DirectReportIds.Contains(t.Id)).ToListAsync();
                if (directReports == null)
                {
                    return new CudResponseDto { IsSucceeded = false };
                }
                toUpdateUser.DirectReports = directReports;
            }

            toUpdateUser.UpdatedAt = DateTime.UtcNow;

            _context.Users.Update(toUpdateUser);
            await _context.SaveChangesAsync();
            return new CudResponseDto
            {
                Id = id,
                IsSucceeded = true
            };
        }
    }
}
