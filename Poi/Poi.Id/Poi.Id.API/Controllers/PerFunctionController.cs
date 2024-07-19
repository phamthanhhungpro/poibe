using Microsoft.AspNetCore.Mvc;
using Poi.Id.Logic.Interfaces.AppPermission;
using Poi.Id.Logic.Requests;
using Poi.Id.Logic.Requests.AppPermission;

namespace Poi.Id.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerFunctionController : ExtendedBaseController
    {
        private readonly IFunctionService _functionService;

        public PerFunctionController(IFunctionService functionService)
        {
            _functionService = functionService;
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] PagingRequest request)
        {
            var result = await _functionService.GetAllAsync(request, TenantInfo);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create(FunctionRequest request)
        {
            var result = await _functionService.CreateAsync(request, TenantInfo);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var result = await _functionService.GetByIdAsync(id, TenantInfo);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, FunctionRequest request)
        {
            var result = await _functionService.UpdateAsync(id, request, TenantInfo);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _functionService.DeleteAsync(id, TenantInfo);
            return Ok(result);
        }

        [HttpPost("assign-api")]
        public async Task<ActionResult> AssignApiToFunction(AssignEndpointToFunctionRequest request)
        {
            var result = await _functionService.AssignApiToFunctionAsync(request, TenantInfo);
            return Ok(result);
        }

        [HttpPost("assign-scope")]
        public async Task<ActionResult> AssignScopeToFunction(AssignScopeToFunctionRequest request)
        {
            var result = await _functionService.AssignScopeToFunctionAsync(request, TenantInfo);
            return Ok(result);
        }

        [HttpGet("withgroup")]
        public async Task<ActionResult> GetFunctionWithGroup()
        {
            var result = await _functionService.GetFunctionWithGroup(TenantInfo);
            return Ok(result);
        }
    }
}
