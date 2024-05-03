using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Poi.Id.Logic.Interfaces;
using Poi.Id.Logic.Requests;
using Poi.Id.Logic.Services;

namespace Poi.Id.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
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
            var apps = await _appService.GetApp(request);
            return Ok(apps);
        }

        // GET api/apps/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetappById(Guid id)
        {
            var app = await _appService.GetAppById(id);
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
    }
}
