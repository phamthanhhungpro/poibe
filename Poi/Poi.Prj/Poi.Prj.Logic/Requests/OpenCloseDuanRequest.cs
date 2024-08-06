using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Prj.Logic.Requests
{
    public class OpenCloseDuanRequest
    {
        public Guid DuanId { get; set; }
        public bool IsClosed { get; set; }
    }
}
