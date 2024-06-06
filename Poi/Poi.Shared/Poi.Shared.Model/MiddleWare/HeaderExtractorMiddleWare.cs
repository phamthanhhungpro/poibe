using Microsoft.AspNetCore.Http;
using Poi.Shared.Model.BaseModel;

namespace Poi.Shared.Model.MiddleWare
{
    public class HeaderExtractorMiddleWare
    {
        private readonly RequestDelegate _next;

        public HeaderExtractorMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            // Extract the tenant ID from the header
            string tenantId = context.Request.Headers["TenantId"];
            string userId = context.Request.Headers["UserId"];
            string role = context.Request.Headers["Role"];

            string path = context.Request.Path.Value;

            if (path.Contains("/api/auth/login"))
            {
                await _next(context);
                return;
            }

            if (path.Contains("/manage/info"))
            {
                await _next(context);
                return;
            }

            if ((string.IsNullOrEmpty(tenantId) || string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(role)))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                await _next(context);
                return;
            }
            var tenantInfo = new TenantInfo
            {
                TenantId = Guid.Parse(tenantId),
                UserId = Guid.Parse(userId),
                Role = role
            };

            // Store the tenant ID in the context items
            context.Items["TenantInfo"] = tenantInfo;

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}
