using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Prj.Logic.Dtos
{
    public class TongQuanDuAnDto
    {
        public string TenDuAn { get; set; }
        public string MoTa { get; set; }
        public string NgayBatDau { get; set; }
        public string NgayKetThuc { get; set; }
        public string TrangThai { get; set; }

        public List<string> ThanhVien { get; set; }
        public TongQuanCongViecDto TongQuanCongViec { get; set; }
    }

    public class TongQuanCongViecDto
    {
        public int SoLuongCongViec { get; set; }
        public int HoanThanh { get; set; }
        public int QuaHan { get; set; }
        public int ChuaBatDau { get; set; }
        public int DangThucHien { get; set; }
    }
}
