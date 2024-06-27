using Poi.Shared.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Id.InfraModel.DataAccess.Prj
{
    public class PrjKanban : BaseEntity
    {
        public string TenCot { get; set; }
        public string MoTa { get; set; }

        public PrjDuAnNvChuyenMon DuAnNvChuyenMon { get; set; }
        public Guid DuAnNvChuyenMonId { get; set; }

        public int TrangThaiCongViec { get; set; }
        public int ThuTu { get; set; }
        public bool YeuCauXacNhan { get; set; }
    }
}
