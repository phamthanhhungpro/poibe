using Microsoft.AspNetCore.Mvc;
using Poi.Hrm.Logic.Interface;
using Poi.Hrm.Logic.Service;

namespace Poi.Hrm.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FaceRecognitionController : ExtendedBaseController
    {
        private readonly IFaceRecognitionService _faceRecognitionService;

        public FaceRecognitionController(IFaceRecognitionService faceRecognitionService)
        {
            _faceRecognitionService = faceRecognitionService;
        }

        [HttpPost("checkin")]
        public async Task<IActionResult> CheckIn([FromBody] ImageDto imageDto)
        {
            if (imageDto == null || string.IsNullOrEmpty(imageDto.Image))
            {
                return BadRequest("Invalid image data");
            }

            byte[] imageBytes = Convert.FromBase64String(imageDto.Image);
            string recognizedName = await _faceRecognitionService.RecognizeAsync(imageBytes, TenantInfo);

            if (recognizedName != "Unknown")
            {
                return Ok(new { Message = recognizedName });
            }
            else
            {
                return BadRequest(new { Message = "Check-in failed: face not recognized" });
            }
        }

        [HttpPost("train")]
        public async Task<IActionResult> Train()
        {
            try
            {
                string datasetDir = Path.Combine("wwwroot", "dataset");
                await _faceRecognitionService.TrainAsync(datasetDir);
                return Ok(new { Message = "Training successful" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Training failed: {ex.Message}" });
            }
        }
    }

    public class ImageDto
    {
        public string Image { get; set; }
    }
}
