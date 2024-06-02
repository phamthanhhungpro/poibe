using Microsoft.AspNetCore.Mvc;
using Poi.Hrm.Logic.Interface;
using Poi.Hrm.Logic.Requests;
using Poi.Hrm.Logic.Service;
using Poi.Id.InfraModel.DataAccess;

namespace Poi.Hrm.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CongThucLuongController : ExtendedBaseController
    {
        private readonly ICongThucLuongService _congThucLuongService;
        public CongThucLuongController(ICongThucLuongService congThucLuongService)
        {
            _congThucLuongService = congThucLuongService;
        }

        [HttpGet("nopaging")]
        public async Task<ActionResult<IEnumerable<HrmCongThucLuong>>> GetNoPaging()
        {
            var result = await _congThucLuongService.GetCongThucLuong(TenantInfo);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HrmCongThucLuong>> GetById(Guid id)
        {
            var result = await _congThucLuongService.GetCongThucLuongById(TenantInfo, id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<HrmCongThucLuong>> Create(CongThucLuongRequest request)
        {
            var res = await _congThucLuongService.CreateCongThucLuong(TenantInfo, request);
            return Ok(res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, CongThucLuongRequest request)
        {
            var res = await _congThucLuongService.UpdateCongThucLuong(id, TenantInfo, request);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _congThucLuongService.DeleteCongThucLuong(TenantInfo, id);

            return Ok(result);
        }
    }
}
