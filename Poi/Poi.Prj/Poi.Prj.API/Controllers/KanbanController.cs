using Microsoft.AspNetCore.Mvc;
using Poi.Prj.Logic.Interface;
using Poi.Prj.Logic.Requests;
using System;
using System.Threading.Tasks;

namespace Poi.Prj.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KanbanController : ExtendedBaseController
    {
        private readonly IKanbanService _kanbanService;

        public KanbanController(IKanbanService kanbanService)
        {
            _kanbanService = kanbanService;
        }

        [HttpGet("nopaging")]
        public async Task<IActionResult> Get(Guid DuanId)
        {
            var result = await _kanbanService.GetNoPaging(TenantInfo, DuanId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _kanbanService.GetByIdAsync(id, TenantInfo);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(KanbanRequest request)
        {
            var result = await _kanbanService.AddAsync(request, TenantInfo);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, KanbanRequest request)
        {
            var result = await _kanbanService.UpdateAsync(id, request, TenantInfo);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _kanbanService.DeleteAsync(id, TenantInfo);
            return Ok(result);
        }
    }
}
