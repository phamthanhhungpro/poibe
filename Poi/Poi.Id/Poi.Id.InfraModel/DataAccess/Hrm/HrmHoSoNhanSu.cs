using Poi.Shared.Model.BaseModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poi.Id.InfraModel.DataAccess.Hrm
{
    public class HrmHoSoNhanSu : BaseEntity
    {
        public string MaHoSo { get; set; }
        public string NgaySinh { get; set; }
        public string TenKhac { get; set; }
        public string NoiSinh { get; set; }
        public string QueQuan { get; set; }
        public string DanToc { get; set; }
        public string TonGiao { get; set; }
        public string ThuongTru { get; set; }
        public string NoiOHienNay { get; set; }

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public virtual HrmKhuVucChuyenMon KhuVucChuyenMon { get; set; }
        public virtual HrmPhanLoaiNhanSu PhanLoaiNhanSu { get; set; }
        public virtual HrmViTriCongViec ViTriCongViec { get; set; }
        public virtual HrmVaiTro VaiTro { get; set; }

        public virtual ChiNhanhVanPhong ChiNhanhVanPhong { get; set; }
        public virtual PhongBanBoPhan PhongBanBoPhan { get; set; }
        public ThongTinNhanSu ThongTinThem { get; set; }
    }

    public class ThongTinNhanSu
    {
        public string NgheNghiepKhiTuyenDung { get; set; }
        public string NgayTuyenDung { get; set; }
        public string CoQuanTuyenDung { get; set; }
        public string ChucVuHienTai { get; set; }
        public string NgayBoNhiem { get; set; }
        public string CongViecChinh { get; set; }
        public string NgachCongChuc { get; set; }
        public string NgayBoNhiemNgach { get; set; }
        public string MaNgach { get; set; }
        public string BacLuong { get; set; }
        public string HeSoLuong { get; set; }
        public string NgayHuongLuong { get; set; }
        public string PhuCapChucVu { get; set; }
        public string PhuCapKhac { get; set; }
        public string TrinhDoGiaoDucPhoThong { get; set; }
        public string TrinhDoChuyenMonCaoNhat { get; set; }
        public string LyLuanChinhTri { get; set; }
        public string QuanLyNhaNuoc { get; set; }
        public string NgoaiNgu { get; set; }
        public string TinHoc { get; set; }
        public string NgayVaoDang { get; set; }
        public string NgayChinhThuc { get; set; }
        public string NgayThamGiaToChucChinhTriXaHoi { get; set; }
        public string NgayNhapNgu { get; set; }
        public string NgayXuatNgu { get; set; }
        public string QuanHamCaoNhat { get; set; }
        public string DanhHieuCaoNhat { get; set; }
        public string SoTruongCongTac { get; set; }
        public string KhenThuong { get; set; }
        public string KyLuat { get; set; }
        public string TinhTrangSucKhoe { get; set; }
        public string ChieuCao { get; set; }
        public string CanNang { get; set; }
        public string NhomMau { get; set; }
        public string ThuongBinhHang { get; set; }
        public string ConGiaDinhChinhSach { get; set; }
        public string SoCMND { get; set; }
        public string NgayCapCMND { get; set; }
        public string SoBHXH { get; set; }
        public string DacDiemLichSuBanThan { get; set; }
        public List<DaoTaoBoDuong> DaoTaoBoDuongs { get; set; }
        public List<QuaTrinhCongTac> QuaTrinhCongTacs { get; set; }
        public List<QuanHeGiaDinh> QuanHeGiaDinhs { get; set; }
        public List<LuongCongChuc> LuongCongChucs { get; set; }
    }

    public class DaoTaoBoDuong
    {
        public string TenTruong { get; set; }
        public string ChuyenNganh { get; set; }
        public string TuNgay { get; set; }
        public string DenNgay { get; set; }
        public string HinhThucDaoTao { get; set; }
        public string VanBang { get; set; }
    }

    public class QuaTrinhCongTac
    {
        public string TuThangNam { get; set; }
        public string DenThangNam { get; set; }
        public string ChucDanhChucVu { get; set; }
        public string DonViCongTac { get; set; }
    }

    public class QuanHeGiaDinh
    {
        public string MoiQuanHe { get; set; }
        public string HoTen { get; set; }
        public string NamSinh { get; set; }
        public string QueQuan { get; set; }
        public string NgheNghiep { get; set; }
    }

    public class LuongCongChuc
    {
        public string ThangNam { get; set; }
        public string MaNgachBac { get; set; }
        public string HeSoLuong { get; set; }
        public bool LaHeSoHienTai { get; set; }
    }
}
