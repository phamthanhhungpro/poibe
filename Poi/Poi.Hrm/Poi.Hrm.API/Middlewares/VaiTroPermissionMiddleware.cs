using Microsoft.EntityFrameworkCore;
using Poi.Id.InfraModel.DataAccess;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Constants;

public class VaiTroPermissionMiddleware
{
    private readonly RequestDelegate _next;

    public VaiTroPermissionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Create a scope to resolve scoped services
        using (var scope = context.RequestServices.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<HrmDbContext>();
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

    private async Task<bool> HasPermission(HttpContext context, HrmDbContext dbContext)
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

        // get the endpoint, method from the request
        var endpoint = context.Request.Path.Value;
        var method = context.Request.Method;

        var chucnang = await dbContext.HrmChucNang.FirstOrDefaultAsync(c => endpoint.Contains(c.Path) && c.Method == method);

        if(chucnang == null)
        {
            return false;
        }

        if (chucnang.IsPublic)
        {
            return true;
        }

        // Get the role of the user
        var user = await dbContext.HrmHoSoNhanSu.Include(hs => hs.VaiTro)
                                                .FirstOrDefaultAsync(u => u.UserId == tenantInfo.UserId);


        if (user == null)
        {
            return false;
        }
        var vaitro = await dbContext.HrmVaiTro
            .Include(vt => vt.HrmNhomChucNang)
            .ThenInclude(nhom => nhom.HrmChucNang)
            .FirstOrDefaultAsync(u => u.Id == user.VaiTro.Id);

        if (vaitro == null)
        {
            return false;
        }

        // Check if the user has the required permission
        if (vaitro.HrmNhomChucNang.Any(p => p.HrmChucNang.Any(c => (c.IsPublic == true) || (endpoint.Contains(c.Path) && c.Method == method))))
        {
            return true;
        }

        return false;
    }
}