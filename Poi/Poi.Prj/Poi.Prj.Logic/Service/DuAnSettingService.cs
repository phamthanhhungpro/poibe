using Poi.Id.InfraModel.DataAccess.Prj;
using Poi.Prj.InfraModel.DataAccess;
using Poi.Prj.Logic.Interface;
using Poi.Prj.Logic.Requests;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Prj.Logic.Service
{
    public class DuAnSettingService : IDuAnSettingService
    {
        private readonly PrjDbContext _context;
        public DuAnSettingService(PrjDbContext context)
        {
            _context = context;
        }

        public async Task<CudResponseDto> AddAsync(DuAnSettingRequest request, TenantInfo info)
        {
            var setting = new PrjDuAnSetting
            {
                DuAnNvChuyenMonId = Guid.NewGuid(),
                Setting = request.Settings.Select(kvp => new Dictionary<string, object> { { kvp.Key, kvp.Value } }).ToList()
            };

            _context.PrjDuAnSetting.Add(setting);
            _context.SaveChanges();

            return new CudResponseDto
            {
                Message = "Success",
                IsSucceeded = true
            };
        }

        public Task<CudResponseDto> DeleteAsync(Guid id, TenantInfo info)
        {
            throw new NotImplementedException();
        }

        public Task<PrjDuAnSetting> GetByIdAsync(Guid id, TenantInfo info)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PrjDuAnSetting>> GetNoPaging(TenantInfo info, Guid DuanId)
        {
            throw new NotImplementedException();
        }

        public Task<CudResponseDto> UpdateAsync(Guid id, DuAnSettingRequest request, TenantInfo info)
        {
            throw new NotImplementedException();
        }
    }
}
