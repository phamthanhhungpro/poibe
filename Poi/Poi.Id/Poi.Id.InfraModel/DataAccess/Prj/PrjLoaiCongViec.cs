using Poi.Shared.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Id.InfraModel.DataAccess.Prj
{
    public class PrjLoaiCongViec : BaseEntity
    {
        public string TenLoaiCongViec { get; set; }
        public string MaLoaiCongViec { get; set; }
        public string MoTa { get; set; }
        public Tenant Tenant { get; set; }
        public Guid TenantId { get; set; }

        public PrjDuAnNvChuyenMon DuAnNvChuyenMon { get; set; }
        public Guid DuAnNvChuyenMonId { get; set; }

        public ICollection<PrjCongViec> CongViec { get; set; }
    }
}
