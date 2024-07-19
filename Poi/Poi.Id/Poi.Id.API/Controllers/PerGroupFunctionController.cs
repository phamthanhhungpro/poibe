using Microsoft.AspNetCore.Mvc;
using Poi.Id.Logic.Interfaces.AppPermission;
using Poi.Id.Logic.Requests.AppPermission;

namespace Poi.Id.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerGroupFunctionController : ExtendedBaseController
    {
        private readonly IGroupFunctionService _groupFunctionService;

        public PerGroupFunctionController(IGroupFunctionService groupFunctionService)
        {
            _groupFunctionService = groupFunctionService;
        }

        [HttpGet("nopaging")]
        public async Task<IActionResult> Get()
        {
            var result = await _groupFunctionService.GetGroupFunctionsAsync(TenantInfo);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(FunctionGroupRequest request)
        {
            var result = await _groupFunctionService.CreateGroupFunctionAsync(request, TenantInfo);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _groupFunctionService.GetGroupFunctionAsync(id);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, FunctionGroupRequest request)
        {
            var result = await _groupFunctionService.UpdateGroupFunctionAsync(id, request);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _groupFunctionService.DeleteGroupFunctionAsync(id);

            return Ok(result);
        }
    }
}
