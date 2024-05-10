using Microsoft.AspNetCore.Mvc;
using Poi.Shared.Model.BaseModel;

namespace Poi.Id.API.Controllers
{
    public class ExtendedBaseController : ControllerBase
    {
        protected TenantInfo TenantInfo => HttpContext.Items["TenantInfo"] as TenantInfo;
    }
}
