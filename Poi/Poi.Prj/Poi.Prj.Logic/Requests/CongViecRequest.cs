namespace Poi.Prj.Logic.Requests
{
    public class CongViecRequest
    {
        public string TenCongViec { get; set; }
        public string MoTa { get; set; }
        public string MaCongViec { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string TrangThai { get; set; }
        public Guid? NhomCongViecId { get; set; }
        public Guid? LoaiCongViecId { get; set; }
        public Guid? CongViecChaId { get; set; }
        public Guid? NguoiDuocGiaoId { get; set; }
        public Guid? NguoiGiaoViecId { get; set; }
        public List<Guid> NguoiPhoiHopIds { get; set; }
        public List<Guid> NguoiThucHienIds { get; set; }
        public List<Guid> TagCongViecIds { get; set; }
        public Guid DuAnNvChuyenMonId { get; set; }
    }
}
