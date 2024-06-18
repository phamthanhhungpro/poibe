using Microsoft.EntityFrameworkCore;
using Poi.Hrm.Logic.Interface;
using Poi.Hrm.Logic.Requests;
using Poi.Id.InfraModel.DataAccess;
using Poi.Id.InfraModel.DataAccess.Hrm;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Hrm.Logic.Service
{
    public class CongThucLuongService : ICongThucLuongService
    {
        private readonly HrmDbContext _hrmDbContext;
        public CongThucLuongService(HrmDbContext hrmDbContext)
        {
            _hrmDbContext = hrmDbContext;
        }

        public async Task<CudResponseDto> CreateCongThucLuong(TenantInfo tenantInfo, CongThucLuongRequest congThucLuong)
        {
            var congThuc = new HrmCongThucLuong
            {
                TenCongThuc = congThucLuong.TenCongThuc,
                ChiTietCongThuc = congThucLuong.ChiTietCongThuc,
                Tenant = _hrmDbContext.Tenants.Find(tenantInfo.TenantId)
            };

            await _hrmDbContext.HrmCongThucLuong.AddAsync(congThuc);
            await _hrmDbContext.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = congThuc.Id
            };
        }

        public async Task<CudResponseDto> DeleteCongThucLuong(TenantInfo tenantInfo, Guid id)
        {
            var congThuc = await _hrmDbContext.HrmCongThucLuong.FindAsync(id);
            if (congThuc == null)
            {
                return new CudResponseDto
                {
                    Id = Guid.Empty
                };
            }

            congThuc.IsDeleted = true;
            congThuc.DeletedAt = DateTime.UtcNow;

            await _hrmDbContext.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = congThuc.Id
            };
        }

        public async Task<List<HrmCongThucLuong>> GetCongThucLuong(TenantInfo tenantInfo)
        {
            return await _hrmDbContext.HrmCongThucLuong
                .Include(x => x.Tenant)
                .Where(x => x.Tenant.Id == tenantInfo.TenantId)
                .ToListAsync();
        }

        public async Task<HrmCongThucLuong> GetCongThucLuongById(TenantInfo tenantInfo, Guid id)
        {
            return await _hrmDbContext.HrmCongThucLuong
                .Include(x => x.Tenant)
                .FirstOrDefaultAsync(x => x.Id == id && x.Tenant.Id == tenantInfo.TenantId);
        }

        public async Task<CudResponseDto> UpdateCongThucLuong(Guid id, TenantInfo tenantInfo, CongThucLuongRequest congThucLuong)
        {
            var congThuc = await _hrmDbContext.HrmCongThucLuong.FindAsync(id);
            if (congThuc == null)
            {
                return new CudResponseDto
                {
                    Id = Guid.Empty
                };
            }

            congThuc.TenCongThuc = congThucLuong.TenCongThuc;
            congThuc.ChiTietCongThuc = congThucLuong.ChiTietCongThuc;

            await _hrmDbContext.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = congThuc.Id
            };
        }
    }
}
