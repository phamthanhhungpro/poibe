using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Poi.Id.InfraModel.DataAccess;
using Poi.Id.Logic.Dtos;
using Poi.Id.Logic.Interfaces;
using Poi.Id.Logic.Requests;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Id.Logic.Services
{
    public class CoQuanDonViService : ICoQuanDonViService
    {

        private readonly IdDbContext _context;
        private readonly IMapper _mapper;
        public CoQuanDonViService(IdDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CudResponseDto> CreateAsync(CoQuanDonViRequest coQuanDonVi, TenantInfo info)
        {
            var entity = _mapper.Map<CoQuanDonVi>(coQuanDonVi);

            entity.Tenant = _context.Tenants.Find(info.TenantId);

            await _context.CoQuanDonVis.AddAsync(entity);
            await _context.SaveChangesAsync();
            return new CudResponseDto { Id = entity.Id };
        }

        public async Task<CudResponseDto> DeleteAsync(Guid id)
        {
            var entity = await _context.CoQuanDonVis.FindAsync(id);
            if (entity == null)
            {
                throw new Exception($"CoQuanDonVi {Error.NotFound}");
            }

            entity.IsDeleted = true;
            entity.UpdatedAt = DateTime.UtcNow;

            _context.CoQuanDonVis.Update(entity);
            await _context.SaveChangesAsync();
            return new CudResponseDto { Id = entity.Id };
        }

        public async Task<IEnumerable<CoQuanDonVi>> GetAllAsync(TenantInfo info)
        {
            var query = _context.CoQuanDonVis
                .Include(x => x.Tenant)
                .Where(x => x.Tenant.Id == info.TenantId);

            return await query.ToListAsync();
        }

        public async Task<CoQuanDonVi> GetByIdAsync(Guid id)
        {
            return await _context.CoQuanDonVis.FindAsync(id);
        }

        public async Task<CudResponseDto> UpdateAsync(Guid id, CoQuanDonViRequest coQuanDonVi)
        {
            var entity = await _context.CoQuanDonVis.FindAsync(id);
            if (entity == null)
            {
                throw new Exception($"CoQuanDonVi {Error.NotFound}");
            }

            _mapper.Map(coQuanDonVi, entity);
            entity.UpdatedAt = DateTime.UtcNow;

            _context.CoQuanDonVis.Update(entity);
            await _context.SaveChangesAsync();
            return new CudResponseDto { Id = entity.Id };
        }
    }
}
