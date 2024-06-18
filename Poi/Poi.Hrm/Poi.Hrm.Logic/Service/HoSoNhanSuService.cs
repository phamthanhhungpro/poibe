using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Poi.Hrm.Logic.Interface;
using Poi.Hrm.Logic.Requests;
using Poi.Id.InfraModel.DataAccess;
using Poi.Id.InfraModel.DataAccess.Hrm;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Constants;
using Poi.Shared.Model.Dtos;
using Poi.Shared.Model.Helpers;

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

            var entity = _mapper.Map<HrmHoSoNhanSu>(hoSoNhanSu);

            entity.MaHoSo = formattedMaHoSo;

            entity.User = await _context.Users.FindAsync(hoSoNhanSu.UserId);

            if (hoSoNhanSu.VaiTroId.HasValue)
            {
                entity.VaiTro = await _context.HrmVaiTro.FindAsync(hoSoNhanSu.VaiTroId);
            }

            if (hoSoNhanSu.ViTriCongViecId.HasValue)
            {
                entity.ViTriCongViec = await _context.HrmViTriCongViec.FindAsync(hoSoNhanSu.ViTriCongViecId);
            }

            if (hoSoNhanSu.ChiNhanhVanPhongId.HasValue)
            {
                entity.ChiNhanhVanPhong = await _context.ChiNhanhVanPhongs.FindAsync(hoSoNhanSu.ChiNhanhVanPhongId);
            }

            if (hoSoNhanSu.PhongBanBoPhanId.HasValue)
            {
                entity.PhongBanBoPhan = await _context.PhongBanBoPhans.FindAsync(hoSoNhanSu.PhongBanBoPhanId);
            }

            await _context.HrmHoSoNhanSu.AddAsync(entity);
            await _context.SaveChangesAsync();

            return new CudResponseDto { Id = entity.Id };
        }

        public async Task<CudResponseDto> DeleteHoSo(TenantInfo tenantInfo, Guid id)
        {
            var model = await _context.HrmHoSoNhanSu.FindAsync(id);

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

        public async Task<List<HrmHoSoNhanSu>> GetHoSo(TenantInfo tenantInfo)
        {
            var data = _context.HrmHoSoNhanSu
                .Include(h => h.User)
                .Include(h => h.VaiTro)
                .Include(h => h.ViTriCongViec)
                .Include(h => h.PhongBanBoPhan)
                .Where(h => h.User.Id == tenantInfo.UserId);

            if (tenantInfo.Role.IsHigherThanAdmin())
            {
                data = _context.HrmHoSoNhanSu.Include(h => h.User)
                                    .Include(h => h.VaiTro)
                                    .Include(h => h.ViTriCongViec)
                                    .Include(h => h.PhongBanBoPhan);
            }

            return await data.ToListAsync();
        }

        public async Task<HrmHoSoNhanSu> GetHoSoById(TenantInfo tenantInfo, Guid id)
        {
            var data = await _context.HrmHoSoNhanSu.Include(h => h.User)
                                                   .Include(h => h.VaiTro)
                                                   .Include(h => h.ViTriCongViec)
                                                   .Include(h => h.PhongBanBoPhan)
                                                   .FirstOrDefaultAsync(x => x.Id == id);

            return data;
        }

        public async Task<CudResponseDto> UpdateHoSo(Guid id, TenantInfo tenantInfo, CreateHoSoNhanSuRequest hoSoNhanSu)
        {
            var model = await _context.HrmHoSoNhanSu.FindAsync(id);

            if (model == null)
            {
                throw new Exception($"HoSoNhanSu {Error.NotFound}");
            }

            _mapper.Map(hoSoNhanSu, model);

            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = model.Id,
                IsSucceeded = true
            };

        }
    }
}
