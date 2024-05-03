using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Poi.Id.Logic.Interfaces;
using Poi.Id.Logic.Requests;

namespace Poi.Id.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // GET api/roles
        [HttpGet]
        public async Task<IActionResult> GetAllRoles([FromQuery] PagingRequest request)
        {
            var roles = await _roleService.GetRole(request);
            return Ok(roles);
        }

        // GET api/roles/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(Guid id)
        {
            var role = await _roleService.GetRoleById(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        // POST api/roles
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] RoleRequest roleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdRole = await _roleService.CreateRole(roleDto);
            return CreatedAtAction(nameof(GetRoleById), new { id = createdRole.Id }, createdRole);
        }

        // PUT api/roles/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(Guid id, [FromBody] RoleRequest roleDto)
        {
            var updatedrole = await _roleService.UpdateRole(id, roleDto);
            if (updatedrole == null)
            {
                return NotFound();
            }
            return Ok(updatedrole);
        }

        // DELETE api/roles/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            var deletedRole = await _roleService.DeleteRole(id);
            if (deletedRole == null)
            {
                return NotFound();
            }
            return Ok(deletedRole);
        }
    }
}
