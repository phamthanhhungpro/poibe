using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Poi.Id.InfraModel.DataAccess;
using Poi.Id.Logic.Dtos;
using Poi.Id.Logic.Interfaces;
using Poi.Id.Logic.Requests;
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
            // generate a new app for me
            var newApp = new App()
            {
                Code = app.Code,
                Name = app.Name,
                Description = app.Description,
            };

            // add the app to the database
            _context.Apps.Add(newApp);
            await _context.SaveChangesAsync();

            // return the new app
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

        public async Task<PagingResponse<App>> GetApp(PagingRequest request)
        {
            var pageSize = request.PageSize;
            var pageNumber = request.PageNumber;

            var query = _context.Apps.OrderByDescending(o => o.CreatedAt).AsNoTracking();

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

        public async Task<CudResponseDto> UpdateApp(Guid id, AppRequest app)
        {
            // find the app
            var toUpdateapp = await _context.Apps.FindAsync(id);

            if (app == null)
            {
                throw new Exception($"app {Error.NotFound}");
            }

            _mapper.Map(app, toUpdateapp);

            toUpdateapp.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = id
            };
        }
    }
}
