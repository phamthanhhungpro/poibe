using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Prj.Logic.Requests
{
    public class TagCongViecRequest
    {
        public string TenTag { get; set; }
        public string MaTag { get; set; }
        public string MoTa { get; set; }
        public Guid DuAnNvChuyenMonId { get; set; }
    }
}
