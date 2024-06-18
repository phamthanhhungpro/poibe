using Poi.Shared.Model.BaseModel;

namespace Poi.Id.InfraModel.DataAccess.Hrm
{
    public class HrmTrangThaiChamCong : BaseEntity
    {
        public string MaTrangThai { get; set; }
        public string TenTrangThai { get; set; }
        public bool YeuCauGiaiTrinh { get; set; }
        public bool TrangThai { get; set; }
        public string MauSac { get; set; }
        public bool IsSystem { get; set; }
        public Tenant Tenant { get; set; }
    }
}
