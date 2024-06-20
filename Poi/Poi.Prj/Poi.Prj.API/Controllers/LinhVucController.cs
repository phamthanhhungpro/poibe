using Microsoft.AspNetCore.Mvc;
using Poi.Prj.Logic.Interface;
using Poi.Prj.Logic.Requests;
using System;
using System.Threading.Tasks;

namespace Poi.Prj.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LinhVucController : ExtendedBaseController
    {
        private readonly ILinhVucService _linhVucService;
        public LinhVucController(ILinhVucService linhVucService)
        {
            _linhVucService = linhVucService;
        }

        [HttpGet("nopaging")]
        public async Task<IActionResult> Get()
        {
            var result = await _linhVucService.GetNoPaging(TenantInfo);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _linhVucService.GetByIdAsync(id, TenantInfo);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LinhVucRequest request)
        {
            var result = await _linhVucService.AddAsync(request, TenantInfo);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] LinhVucRequest request)
        {
            var result = await _linhVucService.UpdateAsync(id, request, TenantInfo);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _linhVucService.DeleteAsync(id, TenantInfo);
            return Ok(result);
        }
    }
}
