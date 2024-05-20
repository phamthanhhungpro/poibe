using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Poi.Shared.Model.BaseModel;

namespace Poi.Id.API.Controllers
{
    [Authorize]
    public class ExtendedBaseController : ControllerBase
    {
        protected TenantInfo TenantInfo => HttpContext.Items["TenantInfo"] as TenantInfo;
    }
}
