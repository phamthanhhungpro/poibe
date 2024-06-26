using Poi.Shared.Model.BaseModel;

namespace Poi.Id.InfraModel.DataAccess.Prj
{
    public class PrjNhomCongViec : BaseEntity
    {
        public string TenNhomCongViec { get; set; }
        public string MoTa { get; set; }
        public string MaNhomCongViec { get; set; }
        public Tenant Tenant { get; set; }
        public Guid TenantId { get; set; }

        public PrjDuAnNvChuyenMon DuAnNvChuyenMon { get; set; }
        public Guid DuAnNvChuyenMonId { get; set; }

        public ICollection<PrjCongViec> CongViec { get; set; }
    }
}
