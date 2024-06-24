using Microsoft.AspNetCore.Mvc;
using Poi.Prj.Logic.Interface;
using Poi.Prj.Logic.Requests;
using System;
using System.Threading.Tasks;

namespace Poi.Prj.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NhomCongViecController : ExtendedBaseController
    {
        private readonly INhomCongViecService _nhomCongViecService;

        public NhomCongViecController(INhomCongViecService nhomCongViecService)
        {
            _nhomCongViecService = nhomCongViecService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid DuanId)
        {
            var result = await _nhomCongViecService.GetNoPaging(TenantInfo, DuanId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _nhomCongViecService.GetByIdAsync(id, TenantInfo);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(NhomCongViecRequest request)
        {
            var result = await _nhomCongViecService.AddAsync(request, TenantInfo);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, NhomCongViecRequest request)
        {
            var result = await _nhomCongViecService.UpdateAsync(id, request, TenantInfo);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _nhomCongViecService.DeleteAsync(id, TenantInfo);
            return Ok(result);
        }
    }
}
