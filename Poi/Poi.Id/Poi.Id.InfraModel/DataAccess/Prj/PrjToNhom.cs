using Poi.Shared.Model.BaseModel;

namespace Poi.Id.InfraModel.DataAccess.Prj
{
    public class PrjToNhom : BaseEntity
    {
        public string TenToNhom { get; set; }
        public string Description { get; set; }
        public Tenant Tenant { get; set; }
        public Guid TenantId { get; set; }

        public virtual ICollection<PrjDuAnNvChuyenMon> DuAn { get; set; }
        public virtual ICollection<User> LanhDao { get; set; }
        public virtual ICollection<User> ThanhVien { get; set; }
    }
}
