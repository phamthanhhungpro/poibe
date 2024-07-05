using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Prj.Logic.Dtos
{
    public class CongViecKanbanDto
    {
        public Guid Id { get; set; }
        public string TenCot { get; set; }
        public string MoTa { get; set; }
        public bool YeuCauXacNhan { get; set; }
        public int ThuTu { get; set; }
        public string TrangThaiCongViec { get; set; }
        public List<CongViecGridDto> ListCongViec { get; set; }
    }
}
