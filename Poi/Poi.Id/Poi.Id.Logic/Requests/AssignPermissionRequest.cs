using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
