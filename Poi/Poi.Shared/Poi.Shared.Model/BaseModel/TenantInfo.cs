﻿namespace Poi.Shared.Model.BaseModel
{
    public class TenantInfo
    {
        public Guid TenantId { get; set; }
        public Guid UserId { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
    }
}
