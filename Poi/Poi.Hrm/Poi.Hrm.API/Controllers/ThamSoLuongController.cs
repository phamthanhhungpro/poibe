using Microsoft.AspNetCore.Mvc;
using Poi.Hrm.Logic.Interface;
using Poi.Hrm.Logic.Requests;
using Poi.Id.InfraModel.DataAccess;

namespace Poi.Hrm.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThamSoLuongController : ExtendedBaseController
    {
        private readonly IThamSoLuongService _thamSoLuongService;

        public ThamSoLuongController(IThamSoLuongService thamSoLuongService)
        {
            _thamSoLuongService = thamSoLuongService;
        }

        [HttpGet("nopaging")]
        public async Task<ActionResult<IEnumerable<HrmThamSoLuong>>> GetNoPaging()
        {
            var result = await _thamSoLuongService.GetThamSoLuong(TenantInfo);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HrmThamSoLuong>> GetById(Guid id)
        {
            var result = await _thamSoLuongService.GetThamSoLuongById(TenantInfo, id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<HrmThamSoLuong>> Create(ThamSoLuongRequest request)
        {
            var res = await _thamSoLuongService.CreateThamSoLuong(TenantInfo, request);
            return Ok(res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, ThamSoLuongRequest request)
        {
            var res = await _thamSoLuongService.UpdateThamSoLuong(id, TenantInfo, request);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _thamSoLuongService.DeleteThamSoLuong(TenantInfo, id);

            return Ok(result);
        }
    }
}