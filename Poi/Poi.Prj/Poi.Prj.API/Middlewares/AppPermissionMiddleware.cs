using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Poi.Prj.InfraModel.DataAccess;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Constants;
using System.Linq;
using System.Threading.Tasks;

namespace Poi.Prj.API.Middlewares
{
    public class AppPermissionMiddleware
    {
        private readonly RequestDelegate _next;

        public AppPermissionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Create a scope to resolve scoped services
            using (var scope = context.RequestServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<PrjDbContext>();
                var checkPermission = await HasPermission(context, dbContext);
                // Now you can use dbContext to check permissions
                if (!checkPermission)
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return;
                }
            }

            await _next(context);
        }

        private async Task<bool> HasPermission(HttpContext context, PrjDbContext dbContext)
        {
            if (dbContext == null)
            {
                return false;
            }

            // Get the tenant info from the context
            TenantInfo tenantInfo = context.Items["TenantInfo"] as TenantInfo;

            if (tenantInfo == null)
            {
                return false;
            }

            // Check if the user is an OWNER
            if (tenantInfo.Role == RoleConstants.ROLE_OWNER)
            {
                return true;
            }

            // Kiểm tra endpoint đã được phân quyền chưa; nếu chưa mặc định cho phép truy cập
            var endpoint = context.Request.Path.Value;
            var method = context.Request.Method;

            var endpointExsit = await dbContext.PerEndpoint.Where(c => c.Path == endpoint && c.Method == method).ToListAsync();

            if (endpointExsit.Count == 0)
            {
                return true;
            }

            var endPointIds = endpointExsit.Select(x => x.Id).ToList();
            // Kiểm tra xem user có quyền truy cập endpoint không

            var user = await dbContext.Users
                .Include(x => x.PerRoles)
                .FirstOrDefaultAsync(x => x.Id == tenantInfo.UserId);

            var userPerRole = user.PerRoles.FirstOrDefault(x => x.AppCode == tenantInfo.AppCode);

            if (userPerRole == null)
            {
                return false;
            }

            var permission = await dbContext.PerRoleFunctionScope
                .Include(x => x.Function)
                .Include(x => x.Scope)
                .Where(c => c.PerRoleId == userPerRole.Id &&
                c.Function.Endpoints.Any(e => e.Path == endpoint && e.Method == method)).ToListAsync();

            if (permission.Count == 0)
            {
                return false;
            }
            else
            {
                var mainPermissions = permission.Where(p => !p.Function.MainEndPointId.HasValue || endPointIds.Contains(p.Function.MainEndPointId.Value)).ToList();

                tenantInfo.IsNeedCheckScope = mainPermissions.Any(p => p.PerScopeId.HasValue);

                tenantInfo.RequestScopeCode = permission.Where(p => p.PerScopeId.HasValue).Select(p => p.Scope.Code).ToList();

                return true;
            }
        }
    }
}
