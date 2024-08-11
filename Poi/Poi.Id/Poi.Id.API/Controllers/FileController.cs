using Microsoft.AspNetCore.Mvc;
using Poi.Shared.Model.Helpers;

namespace Poi.Id.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ExtendedBaseController
    {
        private readonly IWebHostEnvironment _environment;

        public FileController(IWebHostEnvironment environment)
        {
            _environment = environment;

            // Ensure the uploads directory exists
            var uploadsPath = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsPath))
            {
                Directory.CreateDirectory(uploadsPath);
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
                return BadRequest("No files received from the upload.");

            var fileUrls = new List<string>();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var uniqueFileName = StringHelper.Random6charString() + "_" + file.FileName;

                    var filePath = Path.Combine(_environment.WebRootPath, "uploads", uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var fileUrl = $"uploads/{uniqueFileName}";
                    fileUrls.Add(fileUrl);
                }
            }

            return Ok(new { Urls = fileUrls });
        }
    }
}
