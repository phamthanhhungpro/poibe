using Microsoft.AspNetCore.Mvc;
using Poi.Hrm.Logic.Interface;
using Poi.Id.InfraModel.DataAccess.Hrm;

namespace Poi.Hrm.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaiTroController : ExtendedBaseController
    {
        private readonly IVaiTroService _vaiTroService;

        public VaiTroController(IVaiTroService vaiTroService)
        {
            _vaiTroService = vaiTroService;
        }

        [HttpGet("nopaging")]
        public async Task<ActionResult<IEnumerable<HrmVaiTro>>> GetNoPaging()
        {
            var vaiTros = await _vaiTroService.GetNoPaging(TenantInfo);
            return Ok(vaiTros);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HrmVaiTro>> GetById(Guid id)
        {
            var vaiTro = await _vaiTroService.GetByIdAsync(id, TenantInfo);
            if (vaiTro == null)
            {
                return NotFound();
            }
            return Ok(vaiTro);
        }

        [HttpPost]
        public async Task<ActionResult<HrmVaiTro>> Create(HrmVaiTro vaiTro)
        {
            var res = await _vaiTroService.AddAsync(vaiTro, TenantInfo);
            return Ok(res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, HrmVaiTro vaiTro)
        {
            var res = await _vaiTroService.UpdateAsync(id, vaiTro, TenantInfo);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _vaiTroService.DeleteAsync(id, TenantInfo);

            return Ok(result);
        }
    }

}
