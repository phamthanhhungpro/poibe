using Microsoft.AspNetCore.Mvc;
using Poi.Id.Logic.Interfaces;
using Poi.Id.Logic.Requests;

namespace Poi.Id.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoQuanDonViController : ExtendedBaseController
    {
        private readonly ICoQuanDonViService _coQuanDonViService;

        public CoQuanDonViController(ICoQuanDonViService coQuanDonViService)
        {
            _coQuanDonViService = coQuanDonViService;
        }

        // GET api/CoQuanDonVi
        [HttpGet("nopaging")]
        public async Task<IActionResult> Get()
        {
            var coQuanDonVis = await _coQuanDonViService.GetAllAsync(TenantInfo);
            return Ok(coQuanDonVis);
        }

        // GET api/CoQuanDonVi/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var coQuanDonVi = await _coQuanDonViService.GetByIdAsync(id);
            if (coQuanDonVi == null)
            {
                return NotFound();
            }
            return Ok(coQuanDonVi);
        }

        // POST api/CoQuanDonVi
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CoQuanDonViRequest coQuanDonVi)
        {
            if (coQuanDonVi == null)
            {
                return BadRequest();
            }
            var createdCoQuanDonVi = await _coQuanDonViService.CreateAsync(coQuanDonVi, TenantInfo);
            return CreatedAtAction(nameof(Get), new { id = createdCoQuanDonVi.Id }, createdCoQuanDonVi);
        }

        // PUT api/CoQuanDonVi/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] CoQuanDonViRequest coQuanDonVi)
        {
            var result = await _coQuanDonViService.UpdateAsync(id, coQuanDonVi);
            return Ok(result);
        }

        // DELETE api/CoQuanDonVi/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _coQuanDonViService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
