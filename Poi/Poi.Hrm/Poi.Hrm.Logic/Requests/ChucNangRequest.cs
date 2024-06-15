using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Hrm.Logic.Requests
{
    public class ChucNangRequest
    {
        public string TenChucNang { get; set; }
        public string Description { get; set; }
        public string Method { get; set; }
        public string Path { get; set; }
        public bool IsPublic { get; set; }
    }
}
