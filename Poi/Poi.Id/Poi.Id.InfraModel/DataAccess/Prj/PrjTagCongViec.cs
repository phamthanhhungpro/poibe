using Poi.Shared.Model.BaseModel;

namespace Poi.Id.InfraModel.DataAccess.Prj
{
    public class PrjTagCongViec : BaseEntity
    {
        public string TenTag { get; set; }
        public string MaTag { get; set; }
        public Tenant Tenant { get; set; }
        public Guid TenantId { get; set; }
        public PrjDuAnNvChuyenMon DuAnNvChuyenMon { get; set; }
        public Guid DuAnNvChuyenMonId { get; set; }
        public virtual ICollection<PrjCongViec> CongViec { get; set; }
    }
}
