using Poi.Id.InfraModel.DataAccess.Prj;
using Poi.Id.InfraModel.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Prj.Logic.Requests
{
    public class ToNhomRequest
    {
        public string TenToNhom { get; set; }
        public string Description { get; set; }
        public Guid? TruongNhomId { get; set; }
        public List<Guid> MemberIds { get; set; }
    }
}
