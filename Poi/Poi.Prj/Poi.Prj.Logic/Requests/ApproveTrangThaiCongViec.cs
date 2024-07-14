namespace Poi.Prj.Logic.Requests
{
    public class ApproveTrangThaiCongViec
    {
        public Guid DuanId { get; set; }
        public List<Guid> CongViecIds { get; set; }
    }
}
