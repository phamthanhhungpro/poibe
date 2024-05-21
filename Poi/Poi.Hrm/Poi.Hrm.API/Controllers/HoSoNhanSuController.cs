using Microsoft.AspNetCore.Mvc;
using Poi.Hrm.Logic.Interface;
using Poi.Hrm.Logic.Requests;

namespace Poi.Hrm.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HoSoNhanSuController : ExtendedBaseController
    {
        private readonly IHoSoNhanSuService _hoSoNhanSuService;
        public HoSoNhanSuController(IHoSoNhanSuService hoSoNhanSuService)
        {
            _hoSoNhanSuService = hoSoNhanSuService;
        }

        [HttpGet]
        public async Task<IActionResult> GetHoSo()
        {
            var data = await _hoSoNhanSuService.GetHoSo(TenantInfo);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHoSo([FromBody] CreateHoSoNhanSuRequest hoSoNhanSu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var data = await _hoSoNhanSuService.CreateHoSo(TenantInfo, hoSoNhanSu);

            return Ok(data);
        }
    }
}
