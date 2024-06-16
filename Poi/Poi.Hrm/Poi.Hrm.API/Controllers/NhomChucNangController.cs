using Microsoft.AspNetCore.Mvc;
using Poi.Hrm.Logic.Interface;
using Poi.Hrm.Logic.Requests;
using Poi.Id.InfraModel.DataAccess;
using Poi.Shared.Model.Dtos;

namespace Poi.Hrm.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NhomChucNangController : ExtendedBaseController
    {
        private readonly INhomChucNangService _NhomChucNangService;
        public NhomChucNangController(INhomChucNangService NhomChucNangService)
        {
            _NhomChucNangService = NhomChucNangService;
        }

        [HttpPost]
        public async Task<ActionResult<HrmNhomChucNang>> Create(NhomChucNangRequest request)
        {
            var res = await _NhomChucNangService.CreateNhomChucNang(TenantInfo, request);
            return Ok(res);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<HrmNhomChucNang>> Update(Guid id, NhomChucNangRequest request)
        {
            var res = await _NhomChucNangService.UpdateNhomChucNang(id, TenantInfo, request);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<HrmNhomChucNang>> Delete(Guid id)
        {
            var res = await _NhomChucNangService.DeleteNhomChucNang(TenantInfo, id);
            return Ok(res);
        }

        [HttpGet("nopaging")]
        public async Task<ActionResult<List<HrmNhomChucNang>>> Get()
        {
            var res = await _NhomChucNangService.GetNhomChucNang(TenantInfo);
            return Ok(res);
        }

        [HttpGet]
        public async Task<ActionResult<List<HrmNhomChucNang>>> GetPaging([FromQuery] PagingRequest request)
        {
            var res = await _NhomChucNangService.GetPagingNhomChucNang(request, TenantInfo);
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HrmNhomChucNang>> GetById(Guid id)
        {
            var res = await _NhomChucNangService.GetNhomChucNangById(TenantInfo, id);
            return Ok(res);
        }

        [HttpPost("assign-permission")]
        public async Task<ActionResult<CudResponseDto>> AssignPermission(AssignNhomChucNangToVaiTroRequest request)
        {
            var res = await _NhomChucNangService.AssignPermission(request, TenantInfo);
            return Ok(res);
        }

        [HttpPost("assign-chuc-nang")]
        public async Task<ActionResult<CudResponseDto>> AssignChucNang(AssignChucNangToNhomChucNangRequest request)
        {
            var res = await _NhomChucNangService.AssignChucNang(request, TenantInfo);
            return Ok(res);
        }
    }
}
