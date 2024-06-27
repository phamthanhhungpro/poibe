﻿using Poi.Id.InfraModel.DataAccess.Prj;
using Poi.Id.InfraModel.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Prj.Logic.Dtos
{
    public class CongViecGridDto
    {
        public Guid Id { get; set; }
        public string TenCongViec { get; set; }
        public string MoTa { get; set; }
        public string MaCongViec { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string TrangThai { get; set; }
        public User NguoiThucHien { get; set; }
        public User NguoiGiaoViec { get; set; }
        public string CreatedAt { get; set; }
        public IEnumerable<PrjTagCongViec> TagCongViec { get; set; }
    }
}