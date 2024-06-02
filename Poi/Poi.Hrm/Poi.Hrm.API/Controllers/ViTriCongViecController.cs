using Microsoft.AspNetCore.Mvc;
using Poi.Hrm.Logic.Interface;
using Poi.Id.InfraModel.DataAccess;

namespace Poi.Hrm.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViTriCongViecController : ExtendedBaseController
    {
        private readonly IViTriCongViecService _viTriCongViecService;

        public ViTriCongViecController(IViTriCongViecService viTriCongViecService)
        {
            _viTriCongViecService = viTriCongViecService;
        }

        [HttpGet("nopaging")]
        public async Task<ActionResult<IEnumerable<HrmViTriCongViec>>> GetNoPaging()
        {
            var viTriCongViecs = await _viTriCongViecService.GetNoPaging(TenantInfo);
            return Ok(viTriCongViecs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HrmViTriCongViec>> GetById(Guid id)
        {
            var viTriCongViec = await _viTriCongViecService.GetByIdAsync(id, TenantInfo);
            if (viTriCongViec == null)
            {
                return NotFound();
            }
            return Ok(viTriCongViec);
        }

        [HttpPost]
        public async Task<ActionResult<HrmViTriCongViec>> Create(HrmViTriCongViec viTriCongViec)
        {
            var res = await _viTriCongViecService.AddAsync(viTriCongViec, TenantInfo);
            return Ok(res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, HrmViTriCongViec viTriCongViec)
        {
            var res = await _viTriCongViecService.UpdateAsync(id, viTriCongViec, TenantInfo);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _viTriCongViecService.DeleteAsync(id, TenantInfo);

            return Ok(result);
        }
    }

}
