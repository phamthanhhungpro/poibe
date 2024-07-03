using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Id.Logic.Requests
{
    public class FeedbackRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AppName { get; set; }
        public List<IFormFile> Attachments { get; set; }
        public List<string> AttachmentUrls { get; set; }
    }
}
