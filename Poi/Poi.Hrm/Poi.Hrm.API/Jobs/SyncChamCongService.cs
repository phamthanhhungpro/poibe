using Poi.Hrm.Logic.Interface;
using Poi.Hrm.Logic.Requests;
using Poi.Shared.Model.BaseModel;

namespace Poi.Hrm.API.Jobs
{
    public class SyncChamCongService
    {
        private readonly IChamCongDiemDanhService _chamCongDiemDanhService;
        public SyncChamCongService(IChamCongDiemDanhService chamCongDiemDanhService)
        {
            _chamCongDiemDanhService = chamCongDiemDanhService;
        }

        public async Task SyncChamCong()
        {
            var tenantInfo = new TenantInfo
            {
            };

            var request = new ChamCongDiemDanhRequest
            {
                NgayChamCong = DateTime.Now
            };

            await _chamCongDiemDanhService.CreateChamCongDiemDanh(tenantInfo, request);
        }
    }
}
