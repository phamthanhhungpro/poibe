using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Prj.Logic.Requests
{
    public class DanhGiaCongViecRequest
    {
        public Guid CongViecId { get; set; }
        public string DGChatLuongHieuQua { get; set; }
        public string DGTienDo { get; set; }
        public string DGChapHanhCheDoThongTinBaoCao { get; set; }
        public string DGChapHanhDieuDongLamThemGio { get; set; }
    }
}
