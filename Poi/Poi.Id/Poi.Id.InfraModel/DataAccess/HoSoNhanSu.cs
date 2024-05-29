using Poi.Id.InfraModel.DataAccess;
using Poi.Shared.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Id.InfraModel.DataAccess
{
    public class HoSoNhanSu : BaseEntity
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

        public virtual User User { get; set; }
        public ThongTinNhanSu ThongTinThem { get; set; }
    }

    public class ThongTinNhanSu
    {
        public string NgheNghiepKhiTuyenDung { get; set; }
        public DateTime NgayTuyenDung { get; set; }
        public string CoQuanTuyenDung { get; set; }
        public string ChucVuHienTai { get; set; }
        public DateTime NgayBoNhiem { get; set; }
        public string CongViecChinh { get; set; }
        public string NgachCongChuc { get; set; }
        public DateTime NgayBoNhiemNgach { get; set; }
        public string MaNgach { get; set; }
        public decimal BacLuong { get; set; }
        public decimal HeSoLuong { get; set; }
        public DateTime NgayHuongLuong { get; set; }
        public decimal PhuCapChucVu { get; set; }
        public decimal PhuCapKhac { get; set; }
        public string TrinhDoGiaoDucPhoThong { get; set; }
        public string TrinhDoChuyenMonCaoNhat { get; set; }
        public string LyLuanChinhTri { get; set; }
        public string QuanLyNhaNuoc { get; set; }
        public string NgoaiNgu { get; set; }
        public string TinHoc { get; set; }
        public DateTime NgayVaoDang { get; set; }
        public DateTime NgayChinhThuc { get; set; }
        public DateTime NgayThamGiaToChucChinhTriXaHoi { get; set; }
        public DateTime NgayNhapNgu { get; set; }
        public DateTime NgayXuatNgu { get; set; }
        public string QuanHamCaoNhat { get; set; }
        public string DanhHieuCaoNhat { get; set; }
        public string SoTruongCongTac { get; set; }
        public string KhenThuong { get; set; }
        public string KyLuat { get; set; }
        public string TinhTrangSucKhoe { get; set; }
        public decimal ChieuCao { get; set; }
        public decimal CanNang { get; set; }
        public string NhomMau { get; set; }
        public string ThuongBinhHang { get; set; }
        public string ConGiaDinhChinhSach { get; set; }
        public string SoCMND { get; set; }
        public DateTime NgayCapCMND { get; set; }
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
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }
        public string HinhThucDaoTao { get; set; }
        public string VanBang { get; set; }
    }

    public class QuaTrinhCongTac
    {
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }
        public string ChucDanh { get; set; }
        public string DonViCongTac { get; set; }
    }

    public class QuanHeGiaDinh
    {
        public string MoiQuanHe { get; set; }
        public string HoTen { get; set; }
        public int NamSinh { get; set; }
        public string QueQuanNgheNghiep { get; set; }
    }

    public class LuongCongChuc
    {
        public DateTime ThangNam { get; set; }
        public string MaNgachBac { get; set; }
        public decimal HeSoLuong { get; set; }
        public bool LaHeSoHienTai { get; set; }
    }
}
