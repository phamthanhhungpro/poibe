using Microsoft.AspNetCore.Mvc;
using Poi.Id.Logic.Interfaces;
using Poi.Id.Logic.Requests;

namespace Poi.Id.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController(IPermissionService permissionService) : ExtendedBaseController
    {
        private readonly IPermissionService _permissionService = permissionService;

        // GET: api/permission
        [HttpGet]
        public async Task<IActionResult> GetAllPermissions([FromQuery] PagingRequest request)
        {
            var permissions = await _permissionService.GetPermission(request, TenantInfo);
            return Ok(permissions);
        }

        [HttpGet("nopaging")]
        public async Task<IActionResult> GetNoPaging()
        {
            var permissions = await _permissionService.GetNoPaging();
            return Ok(permissions);
        }
        // GET: api/permission/{id}
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetPermissionById(int id)
        //{
        //    var permission = await _permissionService.Get(id);
        //    if (permission == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(permission);
        //}

        // POST: api/permission
        [HttpPost]
        public async Task<IActionResult> CreatePermission([FromBody] CreatePermissionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdPermission = await _permissionService.CreatePermission(request);
            return CreatedAtAction(nameof(CreatePermission), new { id = createdPermission.Id }, createdPermission);
        }

        // PUT: api/permission/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePermission(Guid id, [FromBody] UpdatePermissionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedPermission = await _permissionService.UpdatePermission(id, request);
            if (updatedPermission == null)
            {
                return NotFound();
            }
            return Ok(updatedPermission);
        }

        // DELETE: api/permission/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermission(Guid id)
        {
            var deletedPermission = await _permissionService.DeletePermission(id);
            if (deletedPermission == null)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpPost("assign")]
        public async Task<IActionResult> AssignPermission([FromBody] AssignPermissionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedPermission = await _permissionService.AssignPermission(request);
            if (updatedPermission == null)
            {
                return NotFound();
            }
            return Ok(updatedPermission);
        }
    }
}
