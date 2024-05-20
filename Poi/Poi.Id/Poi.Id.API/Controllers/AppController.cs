using Microsoft.AspNetCore.Mvc;
using Poi.Id.Logic.Interfaces;
using Poi.Id.Logic.Requests;

namespace Poi.Id.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ExtendedBaseController
    {
        private readonly IAppService _appService;

        public AppController(IAppService appService)
        {
            _appService = appService;
        }

        // GET api/app
        [HttpGet]
        public async Task<IActionResult> GetAllApps([FromQuery] PagingRequest request)
        {
            var apps = await _appService.GetApp(request, TenantInfo);
            return Ok(apps);
        }

        // GET api/app
        [HttpGet("nopaging")]
        public async Task<IActionResult> GetAllAppsNoPaging()
        {
            var apps = await _appService.GetAppNoPaging();
            return Ok(apps);
        }

        [HttpGet("byUser")]
        public async Task<IActionResult> GetAppByUser(Guid userId)
        {
            var apps = await _appService.GetAppByUser(userId);
            return Ok(apps);
        }

        // GET api/apps/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetappById(Guid id)
        {
            var app = await _appService.GetAppById(id, TenantInfo);
            if (app == null)
            {
                return NotFound();
            }
            return Ok(app);
        }

        // POST api/apps
        [HttpPost]
        public async Task<IActionResult> CreateApp([FromBody] AppRequest appDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdApp = await _appService.CreateApp(appDto);
            return CreatedAtAction(nameof(GetappById), new { id = createdApp.Id }, createdApp);
        }

        // PUT api/apps/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApp(Guid id, [FromBody] AppRequest appDto)
        {
            var updatedApp = await _appService.UpdateApp(id, appDto);
            if (updatedApp == null)
            {
                return NotFound();
            }
            return Ok(updatedApp);
        }

        // DELETE api/apps/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApp(Guid id)
        {
            var deletedApp = await _appService.DeleteApp(id);
            if (deletedApp == null)
            {
                return NotFound();
            }
            return Ok(deletedApp);
        }

        [HttpPut("appUser/{id}")]
        public async Task<IActionResult> UpdateAppUser(Guid id, [FromBody] UpdateUserAppRequest request)
        {
            var updatedApp = await _appService.UpdateUserApp(id, request, TenantInfo);
            if (updatedApp == null)
            {
                return NotFound();
            }
            return Ok(updatedApp);
        }
    }
}
