using Poi.Shared.Model.BaseModel;

namespace Poi.Id.InfraModel.DataAccess.Hrm
{
    public class HrmDiemDanhHistory : BaseEntity
    {
        public DateTime Time { get; set; }
        public string SnapShotPath { get; set; }
        public User User { get; set; }
    }
}
