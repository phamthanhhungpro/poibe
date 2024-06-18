using Poi.Shared.Model.BaseModel;

namespace Poi.Id.InfraModel.DataAccess.Hrm
{
    public class HrmThamSoLuong : BaseEntity
    {
        public string TenThamSoLuong { get; set; }
        public string MaThamSoLuong { get; set; }
        public string DuongDanTichHop { get; set; }
    }
}
