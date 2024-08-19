using Microsoft.AspNetCore.Mvc;
using Poi.Hrm.Logic.Interface;
using Poi.Hrm.Logic.Requests;
using Poi.Hrm.Logic.Service;
using Poi.Id.InfraModel.DataAccess.Hrm;

namespace Poi.Hrm.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChamCongDiemDanhController : ExtendedBaseController
    {
        private readonly IChamCongDiemDanhService _chamCongDiemDanhService;
        public ChamCongDiemDanhController(IChamCongDiemDanhService chamCongDiemDanhService)
        {
            _chamCongDiemDanhService = chamCongDiemDanhService;
        }

        [HttpPost]
        public async Task<ActionResult<HrmCongKhaiBao>> Create(ChamCongDiemDanhRequest request)
        {
            var res = await _chamCongDiemDanhService.CreateChamCongDiemDanh(TenantInfo, request);
            return Ok(res);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<HrmCongKhaiBao>> GetById(Guid userId, DateTime start, DateTime end)
        {
            var result = await _chamCongDiemDanhService.GetChamCongDiemDanhByUserId(TenantInfo, userId, start, end);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("diem-danh-thu-cong")]
        public async Task<IActionResult> DiemDanhThuCong(DiemDanhThuCongRequest request)
        {
            var res = await _chamCongDiemDanhService.DiemDanhThuCong(TenantInfo, request);
            return Ok(res);
        }
    }
}
