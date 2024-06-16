using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Hrm.Logic.Requests
{
    public class AssignNhomChucNangToVaiTroRequest
    {
        public Guid VaiTroId { get; set; }
        public List<Guid> NhomChucNangIds { get; set; }
    }

    public class AssignChucNangToNhomChucNangRequest
    {
        public Guid NhomChucNangId { get; set; }
        public List<Guid> ChucNangIds { get; set; }
    }
}
