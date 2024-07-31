using Poi.Shared.Model.BaseModel;

namespace Poi.Id.InfraModel.DataAccess.Prj
{
    public class PrjHoatDong : BaseEntity
    {
        public string NoiDung { get; set; }
        public Tenant Tenant { get; set; }
        public Guid TenantId { get; set; }
        public PrjCongViec CongViec { get; set; }
        public Guid CongViecId { get; set; }
        public string UserName { get; set; }
        public string MoreInfo { get; set; }
        public Guid DuanNvChuyenMonId { get; set; }
    }
}
