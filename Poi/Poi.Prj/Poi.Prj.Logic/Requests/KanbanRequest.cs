namespace Poi.Prj.Logic.Requests
{
    public class KanbanRequest
    {
        public string TenCot { get; set; }
        public string MoTa { get; set; }
        public Guid DuAnNvChuyenMonId { get; set; }
        public int TrangThaiCongViec { get; set; }
        public int ThuTu { get; set; }
        public bool YeuCauXacNhan { get; set; }
    }
}
