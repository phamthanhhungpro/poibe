﻿using Poi.Shared.Model.BaseModel;

namespace Poi.Id.InfraModel.DataAccess
{
    public class HrmViTriCongViec : BaseEntity
    {
        public string TenViTri { get; set; }
        public string MoTa { get; set; }
        public Tenant Tenant { get; set; }
    }
}