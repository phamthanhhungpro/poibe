using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Id.Logic.Requests
{
    public class UpdateUserAppRequest
    {
        public List<Guid> UserIds { get; set; }
        public string UserType { get; set; }
    }
}
