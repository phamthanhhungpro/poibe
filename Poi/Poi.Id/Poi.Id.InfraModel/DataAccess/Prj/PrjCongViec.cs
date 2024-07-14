using Poi.Shared.Model.BaseModel;

namespace Poi.Id.InfraModel.DataAccess.Prj
{
    public class PrjCongViec : BaseEntity
    {
        public string TenCongViec { get; set; }
        public string MoTa { get; set; }
        public string MaCongViec { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string TrangThai { get; set; }
        public Tenant Tenant { get; set; }
        public Guid TenantId { get; set; }

        public PrjNhomCongViec NhomCongViec { get; set; }
        public Guid? NhomCongViecId { get; set; }

        public PrjLoaiCongViec LoaiCongViec { get; set; }
        public Guid? LoaiCongViecId { get; set; }

        public PrjCongViec CongViecCha { get; set; }
        public Guid? CongViecChaId { get; set; }

        public User NguoiDuocGiao { get; set; }
        public Guid? NguoiDuocGiaoId { get; set; }

        public User NguoiGiaoViec { get; set; }
        public Guid? NguoiGiaoViecId { get; set; }

        public ICollection<User> NguoiPhoiHop { get; set; }
        public ICollection<User> NguoiThucHien { get; set; }
        public ICollection<PrjTagCongViec> TagCongViec { get; set; }

        public PrjDuAnNvChuyenMon DuAnNvChuyenMon { get; set; }
        public Guid DuAnNvChuyenMonId { get; set; }

        public string ThoiGianDuKien { get; set; }
        public string MucDoUuTien { get; set; }
        public string TrangThaiChiTiet { get; set; }
        public string Attachments { get; set; }
        public DateTime? NgayGiaHan { get; set; }
        public string TrangThaiChoXacNhan { get; set; }

    }
}
