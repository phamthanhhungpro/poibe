using Poi.Id.InfraModel.DataAccess.Prj;
using Poi.Id.InfraModel.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Poi.Prj.Logic.Requests
{
    public class DuAnNvChuyenMonRequest
    {
        public string TenDuAn { get; set; }
        public string MoTaDuAn { get; set; }
        public Guid? ToNhomId { get; set; }
        public Guid? PhongBanBoPhanId { get; set; }
        [Required]
        public Guid? QuanLyDuAnId { get; set; }
        [Required]
        public ICollection<Guid> ThanhVienDuAnIds { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public Guid? LinhVucId { get; set; }
        public bool IsNhiemVuChuyenMon { get; set; }
    }
}
