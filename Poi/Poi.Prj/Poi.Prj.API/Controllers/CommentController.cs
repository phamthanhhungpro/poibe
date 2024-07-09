using Microsoft.AspNetCore.Mvc;
using Poi.Prj.Logic.Interface;
using Poi.Prj.Logic.Requests;
using System.Threading.Tasks;

namespace Poi.Prj.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ExtendedBaseController
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePrjCommentAsync([FromBody] CongViecCommentRequest request)
        {
            var result = await _commentService.CreatePrjCommentAsync(request, TenantInfo);
            return Ok(result);
        }
    }
}
