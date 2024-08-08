using Microsoft.EntityFrameworkCore;
using Poi.Id.InfraModel.DataAccess;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Constants;

public class PermissionCheckMiddleware
{
    private readonly RequestDelegate _next;

    public PermissionCheckMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Create a scope to resolve scoped services
        using (var scope = context.RequestServices.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<IdDbContext>();
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

    private async Task<bool> HasPermission(HttpContext context, IdDbContext dbContext)
    {
        if (dbContext == null)
        {
            return false;
        }
        // temp rule
        if (context.Request.Path.Value.Contains("login"))
        {
            return true;
        }

        if (context.Request.Path.Value.Contains("/manage/info"))
        {
            return true;
        }
        // Get the tenant info from the context
        TenantInfo tenantInfo = context.Items["TenantInfo"] as TenantInfo;

        if (tenantInfo == null)
        {
            return false;
        }

        if (tenantInfo.Role == RoleConstants.ROLE_SSA)
        {
            return true;
        }

        // get the endpoint, method from the request
        var endpoint = context.Request.Path.Value;
        var method = context.Request.Method;

        // If the method and endpoint is not register so It's not required permission
        var isRequiredPermission = await dbContext.Permissions.AnyAsync(x => x.Path == endpoint && x.Method == method);
        if (!isRequiredPermission)
        {
            return true;
        }

        var role = await dbContext.Roles
            .Include(u => u.Permissions)
            .FirstOrDefaultAsync(u => u.Code == tenantInfo.Role);

        if (role == null)
        {
            return false;
        }

        // Check if the user has the required permission
        if (role.Permissions.Any(p => string.Equals(p.Path, endpoint, StringComparison.OrdinalIgnoreCase) && string.Equals(p.Method, method, StringComparison.OrdinalIgnoreCase)))
        {
            return true;
        }

        return false;
    }
}