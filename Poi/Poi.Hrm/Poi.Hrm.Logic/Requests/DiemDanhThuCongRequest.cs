﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Hrm.Logic.Requests
{
    public class DiemDanhThuCongRequest
    {
        public Guid UserId { get; set; }
        public DateTime ThoiGian { get; set; }
        public Guid NguoiXacNhanId { get; set; }
        public Guid CongKhaiBaoId { get; set; }
        public string LyDo { get; set; }
        public string GhiChu { get; set; }
    }
}