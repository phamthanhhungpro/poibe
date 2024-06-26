using Microsoft.AspNetCore.Mvc;
using Poi.Prj.Logic.Interface;
using Poi.Prj.Logic.Requests;
using System;
using System.Threading.Tasks;

namespace Poi.Prj.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DuAnSettingController : ExtendedBaseController
    {
        private readonly IDuAnSettingService _DuAnSettingService;

        public DuAnSettingController(IDuAnSettingService DuAnSettingService)
        {
            _DuAnSettingService = DuAnSettingService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid DuanId)
        {
            var result = await _DuAnSettingService.GetNoPaging(TenantInfo, DuanId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _DuAnSettingService.GetByIdAsync(id, TenantInfo);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(DuAnSettingRequest request)
        {
            var result = await _DuAnSettingService.AddAsync(request, TenantInfo);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, DuAnSettingRequest request)
        {
            var result = await _DuAnSettingService.UpdateAsync(id, request, TenantInfo);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _DuAnSettingService.DeleteAsync(id, TenantInfo);
            return Ok(result);
        }

        [HttpPut("UpdateAllSetting")]
        public async Task<IActionResult> UpdateAllSetting(UpdateDuAnSettingRequest request)
        {
            var result = await _DuAnSettingService.UpdateAllSetting(request, TenantInfo);
            return Ok(result);
        }
    }
}
