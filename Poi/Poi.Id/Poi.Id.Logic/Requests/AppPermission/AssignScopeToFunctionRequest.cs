using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Id.Logic.Requests.AppPermission
{
    public class AssignScopeToFunctionRequest
    {
        public Guid FunctionId { get; set; }
        public List<Guid> ScopeIds { get; set; }
    }
}
