using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Prj.Logic.Requests
{
    public class TagCommentRequest
    {
        public string TenTag { get; set; }
        public string MaTag { get; set; }
        public bool YeuCauXacThuc { get; set; }
        public string MauSac { get; set; }
        public Guid DuAnNvChuyenMonId { get; set; }
    }
}
