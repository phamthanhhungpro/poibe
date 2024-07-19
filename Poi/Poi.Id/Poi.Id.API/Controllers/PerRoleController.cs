using Microsoft.AspNetCore.Mvc;
using Poi.Id.Logic.Interfaces.AppPermission;
using Poi.Id.Logic.Requests.AppPermission;

namespace Poi.Id.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerRoleController : ExtendedBaseController
    {
        private readonly IPerRoleService _perRoleService;

        public PerRoleController(IPerRoleService perRoleService)
        {
            _perRoleService = perRoleService;
        }

        [HttpGet("nopaging")]
        public async Task<ActionResult> Get()
        {
            var result = await _perRoleService.GetRolesAsync(TenantInfo);
            return Ok(result);
        }

        [HttpGet("function-scope-by-role")]
        public async Task<ActionResult> GetFunctionScopeByRole(Guid roleId)
        {
            var result = await _perRoleService.GetFunctionScopeByRole(roleId, TenantInfo);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var result = await _perRoleService.GetRoleAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create(PerRoleRequest request)
        {
            var result = await _perRoleService.CreateRoleAsync(request, TenantInfo);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, PerRoleRequest request)
        {
            var result = await _perRoleService.UpdateRoleAsync(id, request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _perRoleService.DeleteRoleAsync(id);
            return Ok(result);
        }

        [HttpPost("assign-function-to-role")]
        public async Task<ActionResult> AssignFunc(AssignFunctionToRoleRequest request)
        {
            var result = await _perRoleService.AssignFunctionToRole(request, TenantInfo);
            return Ok(result);
        }

        [HttpPost("assign-role-to-user")]
        public async Task<ActionResult> AssignRoleToUser(AssignRoleToUserRequest request)
        {
            var result = await _perRoleService.AssignRoleToUser(request, TenantInfo);
            return Ok(result);
        }
    }
}
