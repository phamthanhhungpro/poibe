using Microsoft.EntityFrameworkCore;
using Poi.Hrm.Logic.Interface;
using Poi.Hrm.Logic.Requests;
using Poi.Id.InfraModel.DataAccess;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Helpers;
using Poi.Shared.Model.Dtos;
using AutoMapper;

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
            var entity = _mapper.Map<HoSoNhanSu>(hoSoNhanSu);

            entity.User = _context.Users.Find(tenantInfo.UserId);

            await _context.HoSoNhanSus.AddAsync(entity);
            await _context.SaveChangesAsync();
            return new CudResponseDto { Id = entity.Id };
        }

        public Task DeleteHoSo(TenantInfo tenantInfo, Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<HoSoNhanSu>> GetHoSo(TenantInfo tenantInfo)
        {
            var data = _context.HoSoNhanSus
                .Include(h => h.User)
                .Where(h => h.User.Id == tenantInfo.UserId);

            if(tenantInfo.Role.IsHigherThanAdmin())
            {
                data = _context.HoSoNhanSus.Include(h => h.User);
            }

            return await data.ToListAsync();
        }

        public Task<HoSoNhanSu> GetHoSoById(TenantInfo tenantInfo, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
