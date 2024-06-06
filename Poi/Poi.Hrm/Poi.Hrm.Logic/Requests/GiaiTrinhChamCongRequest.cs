namespace Poi.Hrm.Logic.Requests
{
    public class GiaiTrinhChamCongRequest
    {
        public Guid ChamCongDiemDanhId { get; set; }
        public Guid CongKhaiBaoId { get; set; }
        public string LyDo { get; set; }
        public Guid NguoiXacNhan { get; set; }
    }
}
