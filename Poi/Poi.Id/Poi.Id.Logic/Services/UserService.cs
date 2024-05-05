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
                    FullName = x.FullName,
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
    }
}
