using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Poi.Id.Logic.Interfaces;
using Poi.Id.Logic.Requests;

namespace Poi.Id.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ExtendedBaseController
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        // GET api/groups
        [HttpGet]
        public async Task<IActionResult> GetAllgroups([FromQuery] PagingRequest request)
        {
            var groups = await _groupService.GetGroup(request, TenantInfo);
            return Ok(groups);
        }

        // GET api/groups/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroupById(Guid id)
        {
            var group = await _groupService.GetGroupById(id);
            if (group == null)
            {
                return NotFound();
            }
            return Ok(group);
        }

        // POST api/groups
        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] GroupRequest groupDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdGroup = await _groupService.CreateGroup(groupDto, TenantInfo);
            return CreatedAtAction(nameof(GetGroupById), new { id = createdGroup.Id }, createdGroup);
        }

        // PUT api/groups/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroup(Guid id, [FromBody] GroupRequest groupDto)
        {
            var updatedGroup = await _groupService.UpdateGroup(id, groupDto, TenantInfo);
            if (updatedGroup == null)
            {
                return NotFound();
            }
            return Ok(updatedGroup);
        }

        // DELETE api/groups/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(Guid id)
        {
            var deletedGroup = await _groupService.DeleteGroup(id);
            if (deletedGroup == null)
            {
                return NotFound();
            }
            return Ok(deletedGroup);
        }
    }
}
