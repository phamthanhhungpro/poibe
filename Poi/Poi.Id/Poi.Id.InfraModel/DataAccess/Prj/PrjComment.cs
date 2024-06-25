using Poi.Shared.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Id.InfraModel.DataAccess.Prj
{
    public class PrjComment : BaseEntity
    {
        public string NoiDung { get; set; }
        public Tenant Tenant { get; set; }
        public Guid TenantId { get; set; }
        public PrjCongViec CongViec { get; set; }
        public Guid CongViecId { get; set; }
        public ICollection<PrjTagComment> TagComments { get; set; }
    }
}
