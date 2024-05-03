using System.ComponentModel.DataAnnotations;

namespace Poi.Id.Logic.Requests
{
    public class CreateTenantRequest
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }

        public List<Guid> AppIds { get; set; }
    }
}
