using Microsoft.AspNetCore.Mvc;
using Poi.Id.Logic.Interfaces;
using Poi.Id.Logic.Requests;

namespace Poi.Id.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenExpiredController : ExtendedBaseController
    {
        private readonly ITokenExpiredService _tokenExpiredService;
        public TokenExpiredController(ITokenExpiredService tokenExpiredService)
        {
            _tokenExpiredService = tokenExpiredService;
        }

        [HttpPost]
        public async Task<IActionResult> AddTokenExpired([FromBody] TokenExpiredRequest request)
        {
            var response = await _tokenExpiredService.AddTokenExpired(request.Token);
            return Ok(response);
        }
    }
}
