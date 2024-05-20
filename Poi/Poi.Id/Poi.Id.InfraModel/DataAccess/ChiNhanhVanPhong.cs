using Poi.Shared.Model.BaseModel;

namespace Poi.Id.InfraModel.DataAccess
{
    public class ChiNhanhVanPhong : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public CoQuanDonVi CoQuanDonVi { get; set; }
        public Tenant Tenant { get; set; }
    }
}
