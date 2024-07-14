namespace Poi.Prj.Logic.Requests
{
    public class ApproveDeXuatCongViec
    {
        public Guid DuanId { get; set; }
        public List<Guid> CongViecIds { get; set; }
    }
}
