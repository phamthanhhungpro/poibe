using Microsoft.EntityFrameworkCore;
using Poi.Id.InfraModel.DataAccess;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Constants;

public class TokenCheckMiddleware
{
    private readonly RequestDelegate _next;

    public TokenCheckMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Create a scope to resolve scoped services
        using (var scope = context.RequestServices.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<HrmDbContext>();
            var isValid = await CheckToken(context, dbContext);
            // Now you can use dbContext to check permissions
            if (!isValid)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }
        }

        await _next(context);
    }

    private async Task<bool> CheckToken(HttpContext context, HrmDbContext dbContext)
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
        // check token expired
        var tokenExpired = await dbContext.TokenExpired.AnyAsync(x => x.Token == tenantInfo.Token);

        if (tokenExpired)
        {
            return false;
        }

        return true;
    }
}