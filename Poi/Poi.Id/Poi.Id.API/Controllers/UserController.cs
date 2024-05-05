using Microsoft.AspNetCore.Mvc;
using Poi.Id.InfraModel.DataAccess;
using Poi.Id.Logic.Interfaces;
using Poi.Id.Logic.Requests;

namespace Poi.Id.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
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
            var data = await _userService.GetUsers(request);
            return Ok(data);
        }

        // GET api/user/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // TODO: Implement logic to retrieve user by id
            return Ok();
        }

        // POST api/user
        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            // TODO: Implement logic to create a new user
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        // PUT api/user/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] User user)
        {
            // TODO: Implement logic to update user by id
            return NoContent();
        }

        // DELETE api/user/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // TODO: Implement logic to delete user by id
            return NoContent();
        }

        [HttpGet("username")]
        public IActionResult GetInfoByUserName([FromQuery] string userName)
        {
            // TODO: Implement logic to retrieve all users
            return Ok();
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
    }
}
