using Microsoft.AspNetCore.Mvc;
using Poi.Hrm.Logic.Interface;
using Poi.Hrm.Logic.Requests;
using Poi.Id.InfraModel.DataAccess.Hrm;

namespace Poi.Hrm.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChucNangController : ExtendedBaseController
    {
        private readonly IChucNangService _chucNangService;
        public ChucNangController(IChucNangService chucNangService)
        {
            _chucNangService = chucNangService;
        }

        [HttpPost]
        public async Task<ActionResult<HrmChucNang>> Create(ChucNangRequest request)
        {
            var res = await _chucNangService.CreateChucNang(TenantInfo, request);
            return Ok(res);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<HrmChucNang>> Update(Guid id, ChucNangRequest request)
        {
            var res = await _chucNangService.UpdateChucNang(id, TenantInfo, request);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<HrmChucNang>> Delete(Guid id)
        {
            var res = await _chucNangService.DeleteChucNang(TenantInfo, id);
            return Ok(res);
        }

        [HttpGet("nopaging")]
        public async Task<ActionResult<List<HrmChucNang>>> GetNoPaging()
        {
            var res = await _chucNangService.GetChucNang(TenantInfo);
            return Ok(res);
        }

        [HttpGet]
        public async Task<ActionResult<List<HrmChucNang>>> GetPaging([FromQuery] PagingRequest request)
        {
            var res = await _chucNangService.GetPagingChucNang(request, TenantInfo);
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HrmChucNang>> GetById(Guid id)
        {
            var res = await _chucNangService.GetChucNangById(TenantInfo, id);
            return Ok(res);
        }
    }
}
