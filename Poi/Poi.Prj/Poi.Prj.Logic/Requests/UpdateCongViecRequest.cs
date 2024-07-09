namespace Poi.Prj.Logic.Requests
{
    public class UpdateCongViecRequest
    {
        public DateTime NgayKetThuc { get; set; }
        public string TrangThai { get; set; }
        public Guid? NguoiDuocGiaoId { get; set; }
        public List<Guid> NguoiPhoiHopIds { get; set; }
        public List<Guid> NguoiThucHienIds { get; set; }
    }
}
