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
    public class ChiNhanhVanPhongService : IChiNhanhVanPhongService
    {

        private readonly IdDbContext _context;
        private readonly IMapper _mapper;
        public ChiNhanhVanPhongService(IdDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CudResponseDto> CreateAsync(ChiNhanhVanPhongRequest coQuanDonVi, TenantInfo info)
        {
            var entity = _mapper.Map<ChiNhanhVanPhong>(coQuanDonVi);

            entity.Tenant = _context.Tenants.Find(info.TenantId);

            await _context.ChiNhanhVanPhongs.AddAsync(entity);
            await _context.SaveChangesAsync();
            return new CudResponseDto { Id = entity.Id };
        }

        public async Task<CudResponseDto> DeleteAsync(Guid id)
        {
            var entity = await _context.ChiNhanhVanPhongs.FindAsync(id);
            if (entity == null)
            {
                throw new Exception($"ChiNhanhVanPhong {Error.NotFound}");
            }

            entity.IsDeleted = true;
            entity.UpdatedAt = DateTime.UtcNow;

            _context.ChiNhanhVanPhongs.Update(entity);
            await _context.SaveChangesAsync();
            return new CudResponseDto { Id = entity.Id };
        }

        public async Task<IEnumerable<ChiNhanhVanPhong>> GetAllAsync(TenantInfo info)
        {
            var query = _context.ChiNhanhVanPhongs
                .Include(x => x.Tenant)
                .Where(x => x.Tenant.Id == info.TenantId);

            return await query.ToListAsync();
        }

        public async Task<ChiNhanhVanPhong> GetByIdAsync(Guid id)
        {
            return await _context.ChiNhanhVanPhongs.FindAsync(id);
        }

        public async Task<CudResponseDto> UpdateAsync(Guid id, ChiNhanhVanPhongRequest coQuanDonVi)
        {
            var entity = await _context.ChiNhanhVanPhongs.FindAsync(id);
            if (entity == null)
            {
                throw new Exception($"ChiNhanhVanPhong {Error.NotFound}");
            }

            _mapper.Map(coQuanDonVi, entity);
            entity.UpdatedAt = DateTime.UtcNow;

            _context.ChiNhanhVanPhongs.Update(entity);
            await _context.SaveChangesAsync();
            return new CudResponseDto { Id = entity.Id };
        }
    }
}
