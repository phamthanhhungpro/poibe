using Microsoft.AspNetCore.Mvc;
using Poi.Shared.Model.BaseModel;

namespace Poi.Prj.API.Controllers
{
    public class ExtendedBaseController : ControllerBase
    {
        protected TenantInfo TenantInfo => HttpContext.Items["TenantInfo"] as TenantInfo;
    }
}
