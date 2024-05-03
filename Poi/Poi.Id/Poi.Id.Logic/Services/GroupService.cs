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
    public class GroupService : IGroupService
    {
        private readonly IdDbContext _context;
        private readonly IMapper _mapper;

        public GroupService(IdDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CudResponseDto> CreateGroup(GroupRequest group)
        {
            var tenant = await _context.Tenants.FindAsync(group.TenantId);

            if (tenant == null)
            {
                throw new Exception($"Tenant {Error.NotFound}");
            }

            var newGroup = new Group
            {
                Name = tenant.Name,
                Description = tenant.Description,
                Code = tenant.Code,
                Tenant = tenant
            };

            newGroup.CreatedAt = DateTime.UtcNow;

            _context.Groups.Add(newGroup);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = newGroup.Id
            };
        }

        public async Task<CudResponseDto> DeleteGroup(Guid id)
        {
            // find the group
            var group = await _context.Groups.FindAsync(id);

            if (group == null)
            {
                throw new Exception($"Group {Error.NotFound}");
            }
            group.DeletedAt = DateTime.UtcNow;
            group.IsDeleted = true;

            await _context.SaveChangesAsync();

            // return the deleted group
            return new CudResponseDto
            {
                Id = group.Id
            };
        }

        public async Task<PagingResponse<Group>> GetGroup(PagingRequest request)
        {
            var pageSize = request.PageSize;
            var pageNumber = request.PageNumber;

            var query = _context.Groups.OrderByDescending(o => o.CreatedAt).AsNoTracking();

            var data = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var count = await query.CountAsync();

            return new PagingResponse<Group>()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Count = count,
                Items = data
            };
        }

        public async Task<Group> GetGroupById(Guid id)
        {
            return await _context.Groups.Include(t => t.Tenant).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<CudResponseDto> UpdateGroup(Guid id, GroupRequest group)
        {
            // find apps
            var tenant = await _context.Tenants.FindAsync(group.TenantId);

            if (tenant == null)
            {
                throw new Exception($"Tenant {Error.NotFound}");
            }

            // find the group
            var toUpdateGroup = await _context.Groups.Include(t => t.Tenant).FirstOrDefaultAsync(t => t.Id == id);

            if (toUpdateGroup == null)
            {
                throw new Exception($"Group {Error.NotFound}");
            }

            _mapper.Map(group, toUpdateGroup);

            toUpdateGroup.Tenant = tenant;

            toUpdateGroup.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = id
            };
        }
    }
}
