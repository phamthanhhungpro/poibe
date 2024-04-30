using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Poi.Id.Logic.Interfaces;
using Poi.Id.Logic.Requests;

namespace Poi.Id.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TenantController : ControllerBase
    {
        private readonly ITenantService _tenantService;

        public TenantController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        // GET api/tenants
        [HttpGet]
        public async Task<IActionResult> GetAllTenants([FromQuery] PagingRequest request)
        {
            var tenants = await _tenantService.GetTenant(request);
            return Ok(tenants);
        }

        // GET api/tenants/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTenantById(Guid id)
        {
            var tenant = await _tenantService.GetTenantById(id);
            if (tenant == null)
            {
                return NotFound();
            }
            return Ok(tenant);
        }

        // POST api/tenants
        [HttpPost]
        public async Task<IActionResult> CreateTenant([FromBody] CreateTenantRequest tenantDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdTenant = await _tenantService.CreateTenant(tenantDto);
            return CreatedAtAction(nameof(GetTenantById), new { id = createdTenant.Id }, createdTenant);
        }

        // PUT api/tenants/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTenant(Guid id, [FromBody] CreateTenantRequest tenantDto)
        {
            var updatedTenant = await _tenantService.UpdateTenant(id, tenantDto);
            if (updatedTenant == null)
            {
                return NotFound();
            }
            return Ok(updatedTenant);
        }

        // DELETE api/tenants/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTenant(Guid id)
        {
            var deletedTenant = await _tenantService.DeleteTenant(id);
            if (deletedTenant == null)
            {
                return NotFound();
            }
            return Ok(deletedTenant);
        }
    }
}
