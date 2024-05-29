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

        [HttpGet("nopaging")]
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHoSo(Guid id)
        {
            var deletedModel = await _hoSoNhanSuService.DeleteHoSo(TenantInfo, id);
            if (deletedModel == null)
            {
                return NotFound();
            }
            return Ok(deletedModel);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHoSoById(Guid id)
        {
            var model = await _hoSoNhanSuService.GetHoSoById(TenantInfo, id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHoSo(Guid id, [FromBody] CreateHoSoNhanSuRequest hoSoNhanSu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var data = await _hoSoNhanSuService.UpdateHoSo(id, TenantInfo, hoSoNhanSu);

            return Ok(data);
        }
    }
}
