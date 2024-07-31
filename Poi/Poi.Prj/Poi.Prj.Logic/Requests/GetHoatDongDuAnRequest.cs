using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Prj.Logic.Requests
{
    public class GetHoatDongDuAnRequest
    {
        public Guid DuanId { get; set; }
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 50;
    }
}
