﻿using Microsoft.AspNetCore.Mvc;
using Poi.Prj.Logic.Interface;
using Poi.Prj.Logic.Requests;
using System;
using System.Threading.Tasks;

namespace Poi.Prj.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DuAnNvChuyenMonController : ExtendedBaseController
    {
        private readonly IDuAnNvChuyenMonService _service;
        public DuAnNvChuyenMonController(IDuAnNvChuyenMonService service)
        {
            _service = service;
        }

        [HttpGet("duan-nopaging")]
        public async Task<IActionResult> GetDuan()
        {
            var result = await _service.GetNoPaging(false, TenantInfo);
            return Ok(result);
        }

        [HttpGet("nvchuyenmon-nopaging")]
        public async Task<IActionResult> GetNvChuyenMon()
        {
            var result = await _service.GetNoPaging(true, TenantInfo);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _service.GetByIdAsync(id, TenantInfo);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DuAnNvChuyenMonRequest request)
        {
            var result = await _service.AddAsync(request, TenantInfo);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] DuAnNvChuyenMonRequest request)
        {
            var result = await _service.UpdateAsync(id, request, TenantInfo);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id, TenantInfo);
            return Ok(result);
        }
    }
}