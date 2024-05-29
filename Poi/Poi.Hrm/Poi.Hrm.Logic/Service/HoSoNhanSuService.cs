using Microsoft.EntityFrameworkCore;
using Poi.Hrm.Logic.Interface;
using Poi.Hrm.Logic.Requests;
using Poi.Id.InfraModel.DataAccess;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Helpers;
using Poi.Shared.Model.Dtos;
using AutoMapper;
using Poi.Shared.Model.Constants;

namespace Poi.Hrm.Logic.Service
{
    public class HoSoNhanSuService : IHoSoNhanSuService
    {
        private readonly HrmDbContext _context;
        private readonly IMapper _mapper;
        public HoSoNhanSuService(HrmDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CudResponseDto> CreateHoSo(TenantInfo tenantInfo, CreateHoSoNhanSuRequest hoSoNhanSu)
        {
            // Use the sequence to get the next value
            var nextId = await _context.Database.ExecuteSqlRawAsync("SELECT nextval('\"HoSoNhanSuSeq\"')");

            var formattedMaHoSo = $"THA-{nextId:0000}";

            var entity = _mapper.Map<HoSoNhanSu>(hoSoNhanSu);

            entity.MaHoSo = formattedMaHoSo;

            entity.User = await _context.Users.FindAsync(hoSoNhanSu.UserId);

            await _context.HoSoNhanSu.AddAsync(entity);
            await _context.SaveChangesAsync();

            return new CudResponseDto { Id = entity.Id };
        }

        public async Task<CudResponseDto> DeleteHoSo(TenantInfo tenantInfo, Guid id)
        {
            var model = await _context.HoSoNhanSu.FindAsync(id);

            if (model == null)
            {
                throw new Exception($"HoSoNhanSu {Error.NotFound}");
            }

            model.IsDeleted = true;
            model.DeletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = model.Id,
                IsSucceeded = true
            };
        }

        public async Task<List<HoSoNhanSu>> GetHoSo(TenantInfo tenantInfo)
        {
            var data = _context.HoSoNhanSu
                .Include(h => h.User)
                .Where(h => h.User.Id == tenantInfo.UserId);

            if(tenantInfo.Role.IsHigherThanAdmin())
            {
                data = _context.HoSoNhanSu.Include(h => h.User);
            }

            return await data.ToListAsync();
        }

        public async Task<HoSoNhanSu> GetHoSoById(TenantInfo tenantInfo, Guid id)
        {
            var data = await _context.HoSoNhanSu.Include(x => x.User)
                                                .FirstOrDefaultAsync(x => x.Id == id);

            return data;
        }
    }
}
