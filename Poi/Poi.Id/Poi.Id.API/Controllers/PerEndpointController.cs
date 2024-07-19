using Microsoft.AspNetCore.Mvc;
using Poi.Id.Logic.Interfaces.AppPermission;
using Poi.Id.Logic.Requests;
using Poi.Id.Logic.Requests.AppPermission;

namespace Poi.Id.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerEndpointController : ExtendedBaseController
    {
        private readonly IPerApiEndpointService _endpointService;
        public PerEndpointController(IPerApiEndpointService endpointService)
        { 
            _endpointService = endpointService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PagingRequest request)
        {
            var result = await _endpointService.GetAllAsync(request, TenantInfo);
            return Ok(result);
        }

        [HttpGet("nopaging")]
        public async Task<IActionResult> GetNoPaging()
        {
            var result = await _endpointService.GetNopaging(TenantInfo);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _endpointService.GetByIdAsync(id, TenantInfo);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApiEndpointRequest request)
        {
            var result = await _endpointService.CreateAsync(request, TenantInfo);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _endpointService.DeleteAsync(id, TenantInfo);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, ApiEndpointRequest request)
        {
            var result = await _endpointService.UpdateAsync(id, request, TenantInfo);
            return Ok(result);
        }
    }
}
