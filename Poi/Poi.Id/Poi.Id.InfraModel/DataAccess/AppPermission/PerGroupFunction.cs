using Poi.Shared.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Id.InfraModel.DataAccess.AppPermission
{
    public class PerGroupFunction : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<PerFunction> Functions { get; set; }
        public string AppCode { get; set; }
    }
}
