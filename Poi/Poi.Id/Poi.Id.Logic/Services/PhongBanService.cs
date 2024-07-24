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
    public class PhongBanService : IPhongBanService
    {

        private readonly IdDbContext _context;
        private readonly IMapper _mapper;
        public PhongBanService(IdDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CudResponseDto> CreateAsync(PhongBanRequest phongBan, TenantInfo info)
        {
            var entity = _mapper.Map<PhongBanBoPhan>(phongBan);

            entity.Tenant = _context.Tenants.Find(info.TenantId);

            if (phongBan.ParentId != null)
            {
                entity.Parent = await _context.PhongBanBoPhans.FindAsync(phongBan.ParentId);
            }

            if (phongBan.ChiNhanhVanPhongId != null)
            {
                entity.ChiNhanhVanPhong = await _context.ChiNhanhVanPhongs.FindAsync(phongBan.ChiNhanhVanPhongId);
            }

            if (phongBan.ManagerIds != null && phongBan.ManagerIds.Count > 0)
            {
                entity.QuanLy = await _context.Users
                    .Include(u => u.Role)
                    .Where(u => phongBan.ManagerIds.Contains(u.Id))
                    //.Where(u => u.Role.Code == RoleConstants.ROLE_ADMIN)
                    .Where(u => u.Tenant.Id == info.TenantId)
                    .ToListAsync();
            }

            await _context.PhongBanBoPhans.AddAsync(entity);
            await _context.SaveChangesAsync();
            return new CudResponseDto { Id = entity.Id };
        }

        public async Task<CudResponseDto> DeleteAsync(Guid id)
        {
            var entity = await _context.PhongBanBoPhans.FindAsync(id);
            if (entity == null)
            {
                throw new Exception($"PhongBanBoPhan {Error.NotFound}");
            }

            entity.IsDeleted = true;
            entity.UpdatedAt = DateTime.UtcNow;

            _context.PhongBanBoPhans.Update(entity);
            await _context.SaveChangesAsync();
            return new CudResponseDto { Id = entity.Id };
        }

        public async Task<IEnumerable<PhongBanBoPhan>> GetAllAsync(TenantInfo info)
        {
            var query = _context.PhongBanBoPhans
                .Include(x => x.Parent)
                .Include(x => x.ChiNhanhVanPhong)
                .Include(x => x.QuanLy)
                .Include(x => x.Tenant)
                .Include(x => x.ThanhVien)
                .Where(x => x.Tenant.Id == info.TenantId);

            return await query.ToListAsync();
        }

        public async Task<PhongBanBoPhan> GetByIdAsync(Guid id)
        {
            return await _context.PhongBanBoPhans
                .Include(x => x.QuanLy)
                .Include(x => x.ThanhVien)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<CudResponseDto> UpdateAsync(Guid id, PhongBanRequest request)
        {
            var entity = await _context.PhongBanBoPhans
                .Include(x => x.Parent)
                .Include(x => x.Tenant)
                .Include(x => x.ChiNhanhVanPhong)
                .Include(x => x.QuanLy)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                throw new Exception($"PhongBanBoPhan {Error.NotFound}");
            }

            _mapper.Map(request, entity);
            entity.UpdatedAt = DateTime.UtcNow;

            if (request.ParentId != null)
            {
                entity.Parent = await _context.PhongBanBoPhans.FindAsync(request.ParentId);
            }

            if (request.ChiNhanhVanPhongId != null)
            {
                entity.ChiNhanhVanPhong = await _context.ChiNhanhVanPhongs.FindAsync(request.ChiNhanhVanPhongId);
            }

            if (request.ManagerIds != null && request.ManagerIds.Count > 0)
            {
                entity.QuanLy = await _context.Users
                    .Include(u => u.Role)
                    .Where(u => request.ManagerIds.Contains(u.Id))
                    //.Where(u => u.Role.Code == RoleConstants.ROLE_ADMIN)
                    .Where(u => u.Tenant.Id == entity.Tenant.Id)
                    .ToListAsync();
            }

            _context.PhongBanBoPhans.Update(entity);
            await _context.SaveChangesAsync();
            return new CudResponseDto { Id = entity.Id };
        }

        public async Task<CudResponseDto> UpdateMember(Guid id, UpdateMemberPhongBanRequest request)
        {
            var entity = await _context.PhongBanBoPhans.Include(x => x.ThanhVien)
                                                       .Include(x => x.Tenant)
                                                       .FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                throw new Exception($"PhongBanBoPhan {Error.NotFound}");
            }

            if (request.UserIds != null && request.UserIds.Count > 0)
            {
                entity.ThanhVien = await _context.Users
                    .Include(u => u.Tenant)
                    .Where(u => request.UserIds.Contains(u.Id))
                    .Where(u => u.Tenant.Id == entity.Tenant.Id)
                    .ToListAsync();
            }

            _context.PhongBanBoPhans.Update(entity);
            await _context.SaveChangesAsync();
            return new CudResponseDto { Id = entity.Id, IsSucceeded = true };
        }
    }
}
