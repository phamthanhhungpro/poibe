using System.ComponentModel.DataAnnotations;

namespace Poi.Id.Logic.Requests
{
    public class AssignPermissionRequest
    {
        [Required]
        public Guid RoleId { get; set; }
        [Required]
        public List<Guid> PermissionIds { get; set; }

    }
}
