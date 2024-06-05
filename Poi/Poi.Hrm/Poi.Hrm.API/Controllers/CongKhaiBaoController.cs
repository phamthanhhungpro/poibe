using Microsoft.AspNetCore.Mvc;
using Poi.Hrm.Logic.Interface;
using Poi.Hrm.Logic.Requests;
using Poi.Hrm.Logic.Service;
using Poi.Id.InfraModel.DataAccess;

namespace Poi.Hrm.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CongKhaiBaoController : ExtendedBaseController
    {
        private readonly ICongKhaiBaoService _congKhaiBaoService;
        public CongKhaiBaoController(ICongKhaiBaoService congKhaiBaoService)
        {
            _congKhaiBaoService = congKhaiBaoService;

        }

        [HttpGet("nopaging")]
        public async Task<ActionResult<IEnumerable<HrmCongKhaiBao>>> GetNoPaging()
        {
            var result = await _congKhaiBaoService.GetCongKhaiBao(TenantInfo);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HrmCongKhaiBao>> GetById(Guid id)
        {
            var result = await _congKhaiBaoService.GetCongKhaiBaoById(TenantInfo, id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<HrmCongKhaiBao>> Create(CongKhaiBaoRequest request)
        {
            var res = await _congKhaiBaoService.CreateCongKhaiBao(TenantInfo, request);
            return Ok(res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, CongKhaiBaoRequest request)
        {
            var res = await _congKhaiBaoService.UpdateCongKhaiBao(id, TenantInfo, request);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _congKhaiBaoService.DeleteCongKhaiBao(TenantInfo, id);

            return Ok(result);
        }

    }
}
