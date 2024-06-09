﻿using Poi.Shared.Model.BaseModel;

namespace Poi.Id.InfraModel.DataAccess
{
    public class HrmGiaiTrinhChamCong : BaseEntity
    {   
        public HrmChamCongDiemDanh HrmChamCongDiemDanh { get; set; }
        public string LyDo { get; set; }
        public HrmCongKhaiBao HrmCongKhaiBao { get; set; }
        public DateTime ThoiGian { get; set; }
        public User NguoiXacNhan { get; set; }
        public bool TrangThai { get; set; }
    }
}