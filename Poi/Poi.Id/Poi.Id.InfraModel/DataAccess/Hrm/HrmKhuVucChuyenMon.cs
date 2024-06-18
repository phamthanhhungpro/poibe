using Poi.Shared.Model.BaseModel;

namespace Poi.Id.InfraModel.DataAccess.Hrm
{
    public class HrmKhuVucChuyenMon : BaseEntity
    {
        public string Ten { get; set; }
        public string MaKhuVuc { get; set; }
        public bool TrangThai { get; set; }
        public Tenant Tenant { get; set; }
    }
}
