using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Id.Logic.Requests.AppPermission
{
    public class AssignEndpointToFunctionRequest
    {
        public Guid FunctionId { get; set; }
        public List<Guid> EndPointIds { get; set; }

        public Guid MainEndPointId { get; set; }
    }
}
