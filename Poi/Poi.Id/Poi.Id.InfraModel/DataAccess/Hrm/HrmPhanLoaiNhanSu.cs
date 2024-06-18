using Poi.Shared.Model.BaseModel;

namespace Poi.Id.InfraModel.DataAccess.Hrm
{
    public class HrmPhanLoaiNhanSu : BaseEntity
    {
        public string Ten { get; set; }
        public string MaPhanLoai { get; set; }
        public bool TrangThai { get; set; }

        public Tenant Tenant { get; set; }
    }
}
