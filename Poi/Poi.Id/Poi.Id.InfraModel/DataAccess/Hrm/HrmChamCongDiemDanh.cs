using Poi.Shared.Model.BaseModel;

namespace Poi.Id.InfraModel.DataAccess.Hrm
{
    public class HrmChamCongDiemDanh : BaseEntity
    {
        public User User { get; set; }
        public Guid UserId { get; set; } // Người chấm công
        public HrmTrangThaiChamCong HrmTrangThaiChamCong { get; set; }
        public HrmCongKhaiBao HrmCongKhaiBao { get; set; }
        public HrmCongKhaiBao HrmCongXacNhan { get; set; }
        public DateTime ThoiGian { get; set; }
        public TrangThaiEnum TrangThai { get; set; }
        public string LyDo { get; set; }
        public string GhiChu { get; set; }
        public Guid? NguoiXacNhanId { get; set; }
        public User NguoiXacNhan { get; set; }
    }

    public enum TrangThaiEnum
    {
        XacNhan = 1,
        ChoGiaiTrinh = 2,
        ChoXacNhan = 3,
    }
}
