using Poi.Id.InfraModel.DataAccess;
using Poi.Shared.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Id.InfraModel.DataAccess
{
    public class KhuVucChuyenMon : BaseEntity
    {
        public string Ten { get; set; }
        public string MaKhuVuc { get; set; }
        public bool TrangThai { get; set; }

    }
}
