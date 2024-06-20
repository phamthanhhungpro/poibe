using Microsoft.AspNetCore.Mvc;
using Poi.Prj.Logic.Interface;
using Poi.Prj.Logic.Requests;
using System;
using System.Threading.Tasks;

namespace Poi.Prj.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToNhomController : ExtendedBaseController
    {
        private readonly IToNhomService _toNhomService;
        public ToNhomController(IToNhomService toNhomService)
        {
            _toNhomService = toNhomService;
        }

        [HttpGet("nopaging")]
        public async Task<IActionResult> Get()
        {
            var result = await _toNhomService.GetNoPaging(TenantInfo);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _toNhomService.GetByIdAsync(id, TenantInfo);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ToNhomRequest request)
        {
            var result = await _toNhomService.AddAsync(request, TenantInfo);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] ToNhomRequest request)
        {
            var result = await _toNhomService.UpdateAsync(id, request, TenantInfo);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _toNhomService.DeleteAsync(id, TenantInfo);
            return Ok(result);
        }
    }
}
