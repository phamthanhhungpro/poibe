using Poi.Id.InfraModel.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Id.Logic.Requests
{
    public class UpdateUserRequest
    {
        public string SurName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Guid? RoleId { get; set; }
        public List<Guid> AppIds { get; set; }
        public Guid? GroupId { get; set; }
        public string IsActive { get; set; }
        public List<Guid> ManagerIds { get; set; } // Collection of managers (users)
        public List<Guid> DirectReportIds { get; set; } // Collection of direct reports (users)
    }
}
