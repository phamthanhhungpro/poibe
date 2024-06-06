using Poi.Shared.Model.BaseModel;

namespace Poi.Id.InfraModel.DataAccess
{
    public class PhongBanBoPhan : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public virtual ICollection<User> QuanLy { get; set; }
        public virtual ICollection<User> ThanhVien { get; set; }
        public virtual ICollection<PhongBanBoPhan> Children { get; set; }
        public PhongBanBoPhan Parent { get; set; }
        public ChiNhanhVanPhong ChiNhanhVanPhong { get; set; }
        public Tenant Tenant { get; set; }
    }
}
