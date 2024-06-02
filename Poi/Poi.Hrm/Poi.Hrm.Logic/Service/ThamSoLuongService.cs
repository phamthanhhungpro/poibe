using Microsoft.EntityFrameworkCore;
using Poi.Hrm.Logic.Interface;
using Poi.Hrm.Logic.Requests;
using Poi.Id.InfraModel.DataAccess;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;

namespace Poi.Hrm.Logic.Service
{
    public class ThamSoLuongService : IThamSoLuongService
    {
        private readonly HrmDbContext _hrmDbContext;

        public ThamSoLuongService(HrmDbContext hrmDbContext)
        {
            _hrmDbContext = hrmDbContext;
        }

        public async Task<CudResponseDto> CreateThamSoLuong(TenantInfo tenantInfo, ThamSoLuongRequest request)
        {
            var thamSoLuong = new HrmThamSoLuong
            {
                TenThamSoLuong = request.TenThamSoLuong,
                MaThamSoLuong = request.MaThamSoLuong,
                DuongDanTichHop = request.DuongDanTichHop
            };

            await _hrmDbContext.HrmThamSoLuong.AddAsync(thamSoLuong);
            await _hrmDbContext.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = thamSoLuong.Id
            };
        }

        public async Task<CudResponseDto> DeleteThamSoLuong(TenantInfo tenantInfo, Guid id)
        {
            var thamSoLuong = await _hrmDbContext.HrmThamSoLuong.FindAsync(id);
            if (thamSoLuong == null)
            {
                return new CudResponseDto
                {
                    Id = Guid.Empty
                };
            }

            thamSoLuong.IsDeleted = true;
            thamSoLuong.DeletedAt = DateTime.UtcNow;

            await _hrmDbContext.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = thamSoLuong.Id
            };
        }

        public async Task<List<HrmThamSoLuong>> GetThamSoLuong(TenantInfo tenantInfo)
        {
            return await _hrmDbContext.HrmThamSoLuong.ToListAsync();
        }

        public async Task<HrmThamSoLuong> GetThamSoLuongById(TenantInfo tenantInfo, Guid id)
        {
            return await _hrmDbContext.HrmThamSoLuong.FindAsync(id);
        }

        public async Task<CudResponseDto> UpdateThamSoLuong(Guid id, TenantInfo tenantInfo, ThamSoLuongRequest request)
        {
            var thamSoLuong = await _hrmDbContext.HrmThamSoLuong.FindAsync(id);
            if (thamSoLuong == null)
            {
                return new CudResponseDto
                {
                    Id = Guid.Empty
                };
            }

            thamSoLuong.TenThamSoLuong = request.TenThamSoLuong;
            thamSoLuong.MaThamSoLuong = request.MaThamSoLuong;
            thamSoLuong.DuongDanTichHop = request.DuongDanTichHop;
            thamSoLuong.UpdatedAt = DateTime.UtcNow;

            await _hrmDbContext.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = thamSoLuong.Id
            };
        }
    }
}
