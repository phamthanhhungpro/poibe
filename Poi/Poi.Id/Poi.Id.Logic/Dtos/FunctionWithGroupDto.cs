using Poi.Id.InfraModel.DataAccess.AppPermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Id.Logic.Dtos
{
    public class FunctionWithGroupDto
    {
        public Guid? GroupId { get; set; }
        public string GroupName { get; set; }

        public List<PerFunction> ListFunction { get; set; }
    }
}
