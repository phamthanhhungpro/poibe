using Microsoft.AspNetCore.Mvc;
using Poi.Id.Logic.Interfaces.AppPermission;
using Poi.Id.Logic.Requests.AppPermission;

namespace Poi.Id.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerScopeController : ExtendedBaseController
    {
        private readonly IScopeService _scopeService;

        public PerScopeController(IScopeService scopeService)
        {
            _scopeService = scopeService;
        }

        [HttpGet("nopaging")]
        public async Task<ActionResult> Get()
        {
            var result = await _scopeService.GetScopesAsync(TenantInfo);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var result = await _scopeService.GetScopeAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ScopeRequest request)
        {
            var result = await _scopeService.CreateScopeAsync(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _scopeService.DeleteScopeAsync(id);
            return Ok(result);
        }
    }
}
