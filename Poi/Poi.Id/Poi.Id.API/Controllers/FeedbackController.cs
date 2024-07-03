using Microsoft.AspNetCore.Mvc;
using Poi.Id.InfraModel.DataAccess;
using Poi.Id.Logic.Interfaces;
using Poi.Id.Logic.Requests;

namespace Poi.Id.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ExtendedBaseController
    {
        private readonly IAFeedbackService _feedbackService;

        public FeedbackController(IAFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost]
        public async Task<IActionResult> PostFeedback([FromForm] FeedbackRequest feedback)
        {
            var screenshotPaths = new List<string>();

            if (feedback.Attachments != null && feedback.Attachments.Count > 0)
            {
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "feedbacks");


                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                foreach (var file in feedback.Attachments)
                {
                    var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    var fileExtension = Path.GetExtension(file.FileName);
                    var newFileName = $"{fileName}_{Guid.NewGuid()}{fileExtension}";
                    var filePath = Path.Combine(uploadPath, newFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    screenshotPaths.Add(filePath);
                }
            }
            feedback.AttachmentUrls = [.. screenshotPaths];

            var result = await _feedbackService.AddAFeedback(feedback, TenantInfo);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _feedbackService.GetAFeedbacks();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _feedbackService.GetAFeedbackById(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, AFeedback request)
        {
            var result = await _feedbackService.UpdateAFeedback(id, request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _feedbackService.DeleteAFeedback(id);
            return Ok(result);
        }
    }
}
