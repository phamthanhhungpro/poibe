using Poi.Shared.Model.BaseModel;

namespace Poi.Id.InfraModel.DataAccess
{
    public class HrmCongThucLuong : BaseEntity
    {
        public string TenCongThuc { get; set; }
        public string ChiTietCongThuc { get; set; }

        public virtual Tenant Tenant { get; set; }
    }
}
