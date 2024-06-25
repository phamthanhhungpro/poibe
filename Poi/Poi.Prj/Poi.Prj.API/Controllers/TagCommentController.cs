using Microsoft.AspNetCore.Mvc;
using Poi.Prj.Logic.Interface;
using Poi.Prj.Logic.Requests;
using System;
using System.Threading.Tasks;

namespace Poi.Prj.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagCommentController : ExtendedBaseController
    {
        private readonly ITagCommentService _TagCommentService;

        public TagCommentController(ITagCommentService TagCommentService)
        {
            _TagCommentService = TagCommentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid DuanId)
        {
            var result = await _TagCommentService.GetNoPaging(TenantInfo, DuanId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _TagCommentService.GetByIdAsync(id, TenantInfo);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(TagCommentRequest request)
        {
            var result = await _TagCommentService.AddAsync(request, TenantInfo);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, TagCommentRequest request)
        {
            var result = await _TagCommentService.UpdateAsync(id, request, TenantInfo);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _TagCommentService.DeleteAsync(id, TenantInfo);
            return Ok(result);
        }
    }
}
