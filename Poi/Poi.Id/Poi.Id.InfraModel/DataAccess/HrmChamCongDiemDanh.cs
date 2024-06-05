using Poi.Shared.Model.BaseModel;

namespace Poi.Id.InfraModel.DataAccess
{
    public class HrmChamCongDiemDanh : BaseEntity
    {
        public User User { get; set; }
        public HrmTrangThaiChamCong HrmTrangThaiChamCong { get; set; }
        public HrmCongKhaiBao HrmCongKhaiBao { get; set; }
        public DateTime ThoiGian { get; set; }
        public TrangThaiEnum TrangThai { get; set; }
    }

    public enum TrangThaiEnum
    {
        XacNhan = 1,
        ChoGiaiTrinh = 2,
        ChoXacNhan = 3,
    }
}
