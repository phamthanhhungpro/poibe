﻿using Poi.Id.InfraModel.DataAccess.Prj;
using Poi.Id.InfraModel.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Prj.Logic.Dtos
{
    public class CongViecCommentDto
    {
        public Guid Id { get; set; }
        public string NguoiComment { get; set; }
        public string NguoiCommentFullName { get; set; }
        public string ThoiGian { get; set; }
        public string NoiDung { get; set; }
        public ICollection<PrjTagComment> TagComments { get; set; }
    }
}
