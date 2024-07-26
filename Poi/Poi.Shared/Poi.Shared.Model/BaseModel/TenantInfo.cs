﻿namespace Poi.Shared.Model.BaseModel
{
    public class TenantInfo
    {
        public Guid TenantId { get; set; }
        public Guid UserId { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public string AppCode { get; set; }

        public List<string> RequestScopeCode { get; set; }
        public bool IsNeedCheckScope { get; set; }
    }
}
