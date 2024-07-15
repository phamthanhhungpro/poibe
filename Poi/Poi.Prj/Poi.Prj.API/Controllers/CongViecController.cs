using Microsoft.AspNetCore.Mvc;
using Poi.Prj.Logic.Interface;
using Poi.Prj.Logic.Requests;
using System;
using System.Threading.Tasks;

namespace Poi.Prj.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CongViecController : ExtendedBaseController
    {
        private readonly ICongViecService _congViecService;

        public CongViecController(ICongViecService congViecService)
        {
            _congViecService = congViecService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid DuanId)
        {
            var result = await _congViecService.GetNoPaging(TenantInfo, DuanId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _congViecService.GetByIdAsync(id, TenantInfo);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CongViecRequest request)
        {
            var result = await _congViecService.AddAsync(request, TenantInfo);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateCongViecRequest request)
        {
            var result = await _congViecService.UpdateAsync(id, request, TenantInfo);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _congViecService.DeleteAsync(id, TenantInfo);
            return Ok(result);
        }

        [HttpGet("GetCongViecGrid")]
        public async Task<IActionResult> GetCongViecGrid(Guid DuanId)
        {
            var result = await _congViecService.GetCongViecGrid(TenantInfo, DuanId);
            return Ok(result);
        }

        [HttpGet("GetCongViecKanban")]
        public async Task<IActionResult> GetCongViecKanban(Guid DuanId)
        {
            var result = await _congViecService.GetCongViecKanban(TenantInfo, DuanId);
            return Ok(result);
        }

        [HttpPut("UpdateKanbanStatus")]
        public async Task<IActionResult> UpdateKanbanStatus(UpdateCongViecKanbanStatusRequest request)
        {
            var result = await _congViecService.UpdateKanbanStatus(TenantInfo, request);
            return Ok(result);
        }

        [HttpPut("GiaHanCongViec")]
        public async Task<IActionResult> GiaHanCongViec(GiaHanCongViecRequest request)
        {
            var result = await _congViecService.GiaHanCongViec(TenantInfo, request);
            return Ok(result);
        }

        [HttpGet("GetCongViecGridByTrangThai")]
        public async Task<IActionResult> GetCongViecGridByTrangThai(Guid DuanId, string trangThai)
        {
            var result = await _congViecService.GetCongViecGridByTrangThai(TenantInfo, DuanId, trangThai);
            return Ok(result);
        }

        [HttpPut("ApproveGiaHanCongViec")]
        public async Task<IActionResult> ApproveGiaHanCongViec(ApproveGiaHanCongViec request)
        {
            var result = await _congViecService.ApproveGiaHanCongViec(TenantInfo, request);
            return Ok(result);
        }

        [HttpPut("ApproveTrangThaiCongViec")]
        public async Task<IActionResult> ApproveTrangThaiCongViec(ApproveTrangThaiCongViec request)
        {
            var result = await _congViecService.ApproveTrangThaiCongViec(TenantInfo, request);
            return Ok(result);
        }

        [HttpPut("ApproveDeXuatCongViec")]
        public async Task<IActionResult> ApproveDeXuatCongViec(ApproveTrangThaiCongViec request)
        {
            var result = await _congViecService.ApproveTrangThaiCongViec(TenantInfo, request);
            return Ok(result);
        }

        [HttpPut("RejectDeXuatCongViec")]
        public async Task<IActionResult> RejectDeXuatCongViec(RejectCongViecRequest request)
        {
            var result = await _congViecService.RejectDeXuatCongViec(TenantInfo, request);
            return Ok(result);
        }

        [HttpPut("RejectGiaHanCongViec")]
        public async Task<IActionResult> RejectGiaHanCongViec(RejectCongViecRequest request)
        {
            var result = await _congViecService.RejectGiaHanCongViec(TenantInfo, request);
            return Ok(result);
        }

        [HttpPut("RejectTrangThaiCongViec")]
        public async Task<IActionResult> RejectTrangThaiCongViec(RejectCongViecRequest request)
        {
            var result = await _congViecService.RejectTrangThaiCongViec(TenantInfo, request);
            return Ok(result);
        }
    }
}
