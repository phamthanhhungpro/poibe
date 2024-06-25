using Microsoft.AspNetCore.Mvc;
using Poi.Prj.Logic.Interface;
using Poi.Prj.Logic.Requests;
using System;
using System.Threading.Tasks;

namespace Poi.Prj.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagCongViecController : ExtendedBaseController
    {
        private readonly ITagCongViecService _TagCongViecService;

        public TagCongViecController(ITagCongViecService TagCongViecService)
        {
            _TagCongViecService = TagCongViecService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid DuanId)
        {
            var result = await _TagCongViecService.GetNoPaging(TenantInfo, DuanId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _TagCongViecService.GetByIdAsync(id, TenantInfo);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(TagCongViecRequest request)
        {
            var result = await _TagCongViecService.AddAsync(request, TenantInfo);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, TagCongViecRequest request)
        {
            var result = await _TagCongViecService.UpdateAsync(id, request, TenantInfo);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _TagCongViecService.DeleteAsync(id, TenantInfo);
            return Ok(result);
        }
    }
}
