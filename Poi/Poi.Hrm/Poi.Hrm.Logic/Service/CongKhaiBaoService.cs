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
    public class CongKhaiBaoService : ICongKhaiBaoService
    {
        private readonly HrmDbContext _hrmDbContext;
        public CongKhaiBaoService(HrmDbContext hrmDbContext)
        {
            _hrmDbContext = hrmDbContext;
        }

        public async Task<CudResponseDto> CreateCongKhaiBao(TenantInfo tenantInfo, CongKhaiBaoRequest CongKhaiBao)
        {
            var congThuc = new HrmCongKhaiBao
            {
                TenCongKhaiBao = CongKhaiBao.TenCongKhaiBao,
                MaCongKhaiBao = CongKhaiBao.MaCongKhaiBao,
                Tenant = _hrmDbContext.Tenants.Find(tenantInfo.TenantId)
            };

            await _hrmDbContext.HrmCongKhaiBao.AddAsync(congThuc);
            await _hrmDbContext.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = congThuc.Id,
                IsSucceeded = true
            };
        }

        public async Task<CudResponseDto> DeleteCongKhaiBao(TenantInfo tenantInfo, Guid id)
        {
            var congThuc = await _hrmDbContext.HrmCongKhaiBao.FindAsync(id);
            if (congThuc == null)
            {
                return new CudResponseDto
                {
                    Id = Guid.Empty,
                    IsSucceeded = false
                };
            }

            congThuc.IsDeleted = true;
            congThuc.DeletedAt = DateTime.UtcNow;

            await _hrmDbContext.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = congThuc.Id,
                IsSucceeded = true
            };
        }

        public async Task<List<HrmCongKhaiBao>> GetCongKhaiBao(TenantInfo tenantInfo)
        {
            return await _hrmDbContext.HrmCongKhaiBao
                .Include(x => x.Tenant)
                .Where(x => x.Tenant.Id == tenantInfo.TenantId || x.IsSystem)
                .ToListAsync();
        }

        public async Task<HrmCongKhaiBao> GetCongKhaiBaoById(TenantInfo tenantInfo, Guid id)
        {
            return await _hrmDbContext.HrmCongKhaiBao
                .Include(x => x.Tenant)
                .FirstOrDefaultAsync(x => x.Id == id && x.Tenant.Id == tenantInfo.TenantId);
        }

        public async Task<CudResponseDto> UpdateCongKhaiBao(Guid id, TenantInfo tenantInfo, CongKhaiBaoRequest CongKhaiBao)
        {
            var congThuc = await _hrmDbContext.HrmCongKhaiBao.FindAsync(id);
            if (congThuc == null)
            {
                return new CudResponseDto
                {
                    Id = Guid.Empty,
                    IsSucceeded = false
                };
            }

            congThuc.TenCongKhaiBao = CongKhaiBao.TenCongKhaiBao;
            congThuc.MaCongKhaiBao = CongKhaiBao.MaCongKhaiBao;

            await _hrmDbContext.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = congThuc.Id,
                IsSucceeded = true
            };
        }
    }
}
