using Poi.Shared.Model.BaseModel;

namespace Poi.Id.InfraModel.DataAccess
{
    public class HrmVaiTro : BaseEntity
    {
        public string TenVaiTro { get; set; }
        public string MoTa { get; set; }
        public ICollection<HrmNhomChucNang> HrmNhomChucNang { get; set; }

        public Tenant Tenant { get; set; }
    }
}
