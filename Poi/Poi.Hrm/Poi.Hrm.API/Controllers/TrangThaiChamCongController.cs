using Microsoft.AspNetCore.Mvc;
using Poi.Hrm.Logic.Interface;
using Poi.Hrm.Logic.Requests;
using Poi.Id.InfraModel.DataAccess.Hrm;

namespace Poi.Hrm.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrangThaiChamCongController : ExtendedBaseController
    {
        private readonly ITrangThaiChamCongService _trangThaiChamCongService;
        public TrangThaiChamCongController(ITrangThaiChamCongService trangThaiChamCongService)
        {
            _trangThaiChamCongService = trangThaiChamCongService;
        }

        [HttpGet("nopaging")]
        public async Task<ActionResult<IEnumerable<HrmTrangThaiChamCong>>> GetNoPaging()
        {
            var result = await _trangThaiChamCongService.GetTrangThaiChamCong(TenantInfo);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HrmTrangThaiChamCong>> GetById(Guid id)
        {
            var result = await _trangThaiChamCongService.GetTrangThaiChamCongById(TenantInfo, id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<HrmTrangThaiChamCong>> Create(TrangThaiChamCongRequest request)
        {
            var res = await _trangThaiChamCongService.CreateTrangThaiChamCong(TenantInfo, request);
            return Ok(res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, TrangThaiChamCongRequest request)
        {
            var res = await _trangThaiChamCongService.UpdateTrangThaiChamCong(id, TenantInfo, request);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _trangThaiChamCongService.DeleteTrangThaiChamCong(TenantInfo, id);

            return Ok(result);
        }
    }
}
