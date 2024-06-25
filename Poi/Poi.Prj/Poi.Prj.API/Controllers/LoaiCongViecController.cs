using Microsoft.AspNetCore.Mvc;
using Poi.Prj.Logic.Interface;
using Poi.Prj.Logic.Requests;
using System;
using System.Threading.Tasks;

namespace Poi.Prj.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoaiCongViecController : ExtendedBaseController
    {
        private readonly ILoaiCongViecService _LoaiCongViecService;

        public LoaiCongViecController(ILoaiCongViecService LoaiCongViecService)
        {
            _LoaiCongViecService = LoaiCongViecService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid DuanId)
        {
            var result = await _LoaiCongViecService.GetNoPaging(TenantInfo, DuanId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _LoaiCongViecService.GetByIdAsync(id, TenantInfo);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(LoaiCongViecRequest request)
        {
            var result = await _LoaiCongViecService.AddAsync(request, TenantInfo);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, LoaiCongViecRequest request)
        {
            var result = await _LoaiCongViecService.UpdateAsync(id, request, TenantInfo);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _LoaiCongViecService.DeleteAsync(id, TenantInfo);
            return Ok(result);
        }
    }
}
