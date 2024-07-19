using Poi.Id.InfraModel.DataAccess.AppPermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Id.Logic.Requests.AppPermission
{
    public class FunctionRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
        public Guid GroupFunctionId { get; set; }
        public string AppCode { get; set; }
    }
}
