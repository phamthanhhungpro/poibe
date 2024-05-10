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
    public class AppService : IAppService
    {
        private readonly IdDbContext _context;
        private readonly IMapper _mapper;

        public AppService(IdDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CudResponseDto> CreateApp(AppRequest app)
        {
            var newApp = new App()
            {
                Code = app.Code,
                Name = app.Name,
                Description = app.Description,
            };

            newApp.CreatedAt = DateTime.UtcNow;

            _context.Apps.Add(newApp);
            await _context.SaveChangesAsync();

            return new CudResponseDto()
            {
                Id = newApp.Id,
            };
        }

        public async Task<CudResponseDto> DeleteApp(Guid id)
        {
            // find the app
            var app = await _context.Apps.FindAsync(id);

            if (app == null)
            {
                throw new Exception($"app {Error.NotFound}");
            }
            app.DeletedAt = DateTime.UtcNow;
            app.IsDeleted = true;

            await _context.SaveChangesAsync();

            // return the deleted app
            return new CudResponseDto
            {
                Id = app.Id
            };
        }

        public async Task<PagingResponse<App>> GetApp(PagingRequest request, TenantInfo info)
        {
            var pageSize = request.PageSize;
            var pageNumber = request.PageNumber;

            var query = _context.Apps
                .Include(a => a.Users)
                .OrderByDescending(o => o.CreatedAt).AsNoTracking();

            if (info.Role != RoleConstants.ROLE_SSA)
            {
                query = _context.Users
                    .Where(u => u.Id == info.UserId)
                    .SelectMany(u => u.Apps)
                    .Include(a => a.Users)
                    .OrderByDescending(o => o.CreatedAt).AsNoTracking();
            }

            var data = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var count = await query.CountAsync();

            return new PagingResponse<App>()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Count = count,
                Items = data
            };
        }

        public async Task<App> GetAppById(Guid id)
        {
            return await _context.Apps.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IList<App>> GetAppByUser(Guid userId)
        {
            var data = _context.Apps
                .Include(a => a.Users)
                .Where(a => a.Users.Any(au => au.Id == userId))
                .AsNoTracking();

            return await data.ToListAsync();
        }

        public async Task<IList<App>> GetAppNoPaging()
        {
            return await _context.Apps.AsNoTracking().ToListAsync();
        }

        public async Task<CudResponseDto> UpdateApp(Guid id, AppRequest app)
        {
            // find the app
            var toUpdateApp = await _context.Apps.FindAsync(id);

            if (toUpdateApp == null)
            {
                throw new Exception($"app {Error.NotFound}");
            }

            _mapper.Map(app, toUpdateApp);

            toUpdateApp.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = id
            };
        }
    }
}
