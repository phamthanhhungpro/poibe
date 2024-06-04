using Poi.Shared.Model.BaseModel;

namespace Poi.Id.InfraModel.DataAccess
{
    public class HrmDiemDanhHistory : BaseEntity
    {
        public DateTime Time { get; set; }
        public User User { get; set; }
    }
}
