using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Id.Logic.Requests.AppPermission
{
    public class AssignRoleToUserRequest
    {
        public Guid UserId { get; set; }
        public Guid PerRoleId { get; set; }
    }
}
