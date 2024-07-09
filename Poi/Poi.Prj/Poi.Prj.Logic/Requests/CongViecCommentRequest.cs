using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Prj.Logic.Requests
{
    public class CongViecCommentRequest
    {
        public Guid DuAnId { get; set; }
        public Guid CongViecId { get; set; }
        public string NoiDung { get; set; }
    }
}
