using Microsoft.AspNetCore.Mvc;
using Poi.Id.InfraModel.DataAccess;
using Poi.Id.Logic.Interfaces;
using Poi.Id.Logic.Requests;

namespace Poi.Id.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ExtendedBaseController
    {
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _environment;
        public UserController(IUserService userService, IWebHostEnvironment environment)
        {
            _userService = userService;
            _environment = environment;

        }
        // Post api/user/list
        [HttpPost("list")]
        public async Task<IActionResult> GetListUser([FromBody] PagingRequest request)
        {
            // TODO: Implement logic to retrieve all users
            var data = await _userService.GetUsers(request, TenantInfo);
            return Ok(data);
        }

        // GET api/user/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _userService.GetUserById(id);
            return Ok(response);
        }

        // PUT api/user/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _userService.UpdateUser(id, request);

            return Ok(response);
        }

        // DELETE api/user/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _userService.DeleteUser(id);
            return Ok(response);
        }

        [HttpPost("avatar")]
        public async Task<IActionResult> UploadAvatar()
        {
            try
            {
                if (Request.Form.Files.Count == 0)
                    return BadRequest("No file uploaded.");

                var file = Request.Form.Files[0];

                if (file.Length == 0)
                    return BadRequest("Empty file uploaded.");

                if (file.Length > 2 * 1024 * 1024) // Limit file size to 2 MB
                    return BadRequest("File size exceeds the limit.");

                // Ensure the file has a valid extension
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(fileExtension))
                    return BadRequest("Invalid file type.");


                var fileName = Guid.NewGuid().ToString() + fileExtension;

                // Construct the physical directory path
                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "avatar");

                // Create directory if it doesn't exist
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Construct the physical file path
                var filePath = Path.Combine(directoryPath, fileName);

                var relativeUrl = $"/avatar/{fileName}";
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return Ok(new { avatarUrl = relativeUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        // GET api/user/{id}
        [HttpGet("username")]
        public async Task<IActionResult> GetByUserName([FromQuery] string userName)
        {
            var response = await _userService.GetByUserName(userName, TenantInfo);
            return Ok(response);
        }

        // GET api/user/{id}
        [HttpGet("can-be-manager")]
        public async Task<IActionResult> GetListCanbeManager([FromQuery] Guid userId, [FromQuery] Guid userTenantId)
        {
            var response = await _userService.GetListCanBeManager(userId, userTenantId, TenantInfo);
            return Ok(response);
        }

        [HttpGet("appadmin")]
        public async Task<IActionResult> GetAppAdmin()
        {
            var response = await _userService.GetListAppAdmin(TenantInfo);
            return Ok(response);
        }

        [HttpGet("member")]
        public async Task<IActionResult> GetMember()
        {
            var response = await _userService.GetListMember(TenantInfo);
            return Ok(response);
        }

        [HttpGet("admin")]
        public async Task<IActionResult> GetAdmin()
        {
            var response = await _userService.GetListAdmin(TenantInfo);
            return Ok(response);
        }
    }
}
