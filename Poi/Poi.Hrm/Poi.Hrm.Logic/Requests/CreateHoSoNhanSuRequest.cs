using Poi.Id.InfraModel.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Hrm.Logic.Requests
{
    public class CreateHoSoNhanSuRequest
    {
        [Required]
        public string MaHoSo { get; set; }
        public string NgaySinh { get; set; }
        public string TenKhac { get; set; }
        public string NoiSinh { get; set; }
        public string QueQuan { get; set; }
        public string DanToc { get; set; }
        public string TonGiao { get; set; }
        public string ThuongTru { get; set; }
        public string NoiOHienNay { get; set; }
        public Guid UserId { get; set; }
    }
}
