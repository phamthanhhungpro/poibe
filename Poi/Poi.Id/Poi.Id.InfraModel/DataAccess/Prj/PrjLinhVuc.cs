using Poi.Shared.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Id.InfraModel.DataAccess.Prj
{
    public class PrjLinhVuc : BaseEntity
    {
        public string TenLinhVuc { get; set; }
        public string Description { get; set; }
        public Tenant Tenant { get; set; }
        public Guid TenantId { get; set; }
    }
}
