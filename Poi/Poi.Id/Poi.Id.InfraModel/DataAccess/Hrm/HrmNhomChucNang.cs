using Poi.Shared.Model.BaseModel;

namespace Poi.Id.InfraModel.DataAccess.Hrm
{
    public class HrmNhomChucNang : BaseEntity
    {
        public string TenNhomChucNang { get; set; }
        public string MoTa { get; set; }
        public bool IsSystem { get; set; }
        public Tenant Tenant { get; set; }
        public Guid TenantId { get; set; }
        public ICollection<HrmChucNang> HrmChucNang { get; set; }
        public ICollection<HrmVaiTro> HrmVaiTro { get; set; }
    }
}
