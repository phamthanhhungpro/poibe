namespace Poi.Prj.Logic.Requests
{
    public class ApproveGiaHanCongViec
    {
        public Guid DuanId { get; set; }
        public List<Guid> CongViecIds { get; set; }
    }
}
