using Microsoft.AspNetCore.Mvc;
using Poi.Hrm.Logic.Interface;
using Poi.Hrm.Logic.Requests;
using Poi.Hrm.Logic.Service;
using Poi.Id.InfraModel.DataAccess;

namespace Poi.Hrm.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GiaiTrinhChamCongController : ExtendedBaseController
    {
        private readonly IGiaiTrinhChamCongService _giaiTrinhChamCongService;
        public GiaiTrinhChamCongController(IGiaiTrinhChamCongService giaiTrinhChamCongService)
        {
            _giaiTrinhChamCongService = giaiTrinhChamCongService;
        }

        [HttpPost]
        public async Task<ActionResult<HrmGiaiTrinhChamCong>> Create(GiaiTrinhChamCongRequest request)
        {
            var res = await _giaiTrinhChamCongService.CreateGiaiTrinhChamCong(TenantInfo, request);
            return Ok(res);
        }
    }
}
