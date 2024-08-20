using Poi.Id.InfraModel.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Id.Logic.Dtos
{
    public class UserPhongBanDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }

        public PhongBanBoPhan PhongBanBoPhan { get; set; }
    }
}
