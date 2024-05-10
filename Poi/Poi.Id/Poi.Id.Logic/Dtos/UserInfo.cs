using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Id.Logic.Dtos
{
    public class UserInfo
    {
        public Guid Id { get; set; }
        public Guid? TenantId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
    }
}
