using Poi.Shared.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Id.InfraModel.DataAccess.Hrm
{
    public class HrmChucNang : BaseEntity
    {
        public string TenChucNang { get; set; }
        public string Description { get; set; }
        public string Method { get; set; }
        public string Path { get; set; }
        public bool IsPublic { get; set; }
        public ICollection<HrmNhomChucNang> HrmNhomChucNang { get; set; }
    }
}
