using Poi.Shared.Model.BaseModel;

namespace Poi.Id.InfraModel.DataAccess
{
    public class HrmCongKhaiBao : BaseEntity
    {
        public string TenCongKhaiBao { get; set; }
        public string MaCongKhaiBao { get; set; }
        public Tenant Tenant { get; set; }
    }
}
