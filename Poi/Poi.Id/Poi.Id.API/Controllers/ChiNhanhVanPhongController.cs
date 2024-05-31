using Microsoft.AspNetCore.Mvc;
using Poi.Id.Logic.Interfaces;
using Poi.Id.Logic.Requests;

namespace Poi.Id.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChiNhanhVanPhongController : ExtendedBaseController
    {
        private readonly IChiNhanhVanPhongService _chiNhanhVanPhongService;

        public ChiNhanhVanPhongController(IChiNhanhVanPhongService chiNhanhVanPhongService)
        {
            _chiNhanhVanPhongService = chiNhanhVanPhongService;
        }

        [HttpGet("nopaging")]
        public async Task<IActionResult> Get()
        {
            var result = await _chiNhanhVanPhongService.GetAllAsync(TenantInfo);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _chiNhanhVanPhongService.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ChiNhanhVanPhongRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }
            var result = await _chiNhanhVanPhongService.CreateAsync(request, TenantInfo);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] ChiNhanhVanPhongRequest request)
        {
            var result = await _chiNhanhVanPhongService.UpdateAsync(id, request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _chiNhanhVanPhongService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
