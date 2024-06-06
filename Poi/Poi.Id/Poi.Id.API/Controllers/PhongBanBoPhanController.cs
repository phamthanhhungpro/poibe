using Microsoft.AspNetCore.Mvc;
using Poi.Id.Logic.Interfaces;
using Poi.Id.Logic.Requests;
using Poi.Id.Logic.Services;

namespace Poi.Id.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhongBanBoPhanController : ExtendedBaseController
    {
        private readonly IPhongBanService _phongBanBoPhanService;

        public PhongBanBoPhanController(IPhongBanService phongBanBoPhanService)
        {
            _phongBanBoPhanService = phongBanBoPhanService;
        }

        // GET api/PhongBanBoPhan
        [HttpGet("nopaging")]
        public async Task<IActionResult> Get()
        {
            var phongBanBoPhans = await _phongBanBoPhanService.GetAllAsync(TenantInfo);
            return Ok(phongBanBoPhans);
        }

        // GET api/PhongBanBoPhan/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var phongBanBoPhan = await _phongBanBoPhanService.GetByIdAsync(id);
            if (phongBanBoPhan == null)
            {
                return NotFound();
            }
            return Ok(phongBanBoPhan);
        }

        // POST api/PhongBanBoPhan
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PhongBanRequest phongBanBoPhan)
        {
            if (phongBanBoPhan == null)
            {
                return BadRequest();
            }
            var createdPhongBanBoPhan = await _phongBanBoPhanService.CreateAsync(phongBanBoPhan, TenantInfo);
            return CreatedAtAction(nameof(Get), new { id = createdPhongBanBoPhan.Id }, createdPhongBanBoPhan);
        }

        // PUT api/PhongBanBoPhan/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] PhongBanRequest phongBanBoPhan)
        {
            var result = await _phongBanBoPhanService.UpdateAsync(id, phongBanBoPhan);
            return Ok(result);
        }

        // DELETE api/PhongBanBoPhan/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _phongBanBoPhanService.DeleteAsync(id);
            return Ok(result);
        }

        [HttpPut("member/{id}")]
        public async Task<IActionResult> UpdateMember(Guid id, [FromBody] UpdateMemberPhongBanRequest request)
        {
            var updatedApp = await _phongBanBoPhanService.UpdateMember(id, request);
            if (updatedApp == null)
            {
                return NotFound();
            }
            return Ok(updatedApp);
        }
    }
}
