using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Id.Logic.Requests.AppPermission
{
    public class AssignFunctionToRoleRequest
    {
        public Guid RoleId { get; set; }

        public List<FunctionScopeRequest> FunctionScopes { get; set; }
    }

    public class FunctionScopeRequest
    {
        public Guid FunctionId { get; set; }
        public Guid? ScopeId { get; set; }
    }
}
