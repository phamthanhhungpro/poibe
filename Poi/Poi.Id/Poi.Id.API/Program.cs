using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Poi.Id.API.Controllers;
using Poi.Id.InfraModel.DataAccess;
using Poi.Id.Logic;
using Poi.Id.Logic.Interfaces;
using Poi.Id.Logic.Interfaces.AppPermission;
using Poi.Id.Logic.Services;
using Poi.Id.Logic.Services.AppPermission;
using Poi.Shared.Model.MiddleWare;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<IdDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Poi ID", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

builder.Services.AddIdentityApiEndpoints<User>().AddEntityFrameworkStores<IdDbContext>();

builder.Services.AddOptions<BearerTokenOptions>(IdentityConstants.BearerScheme).Configure(options => {
    options.BearerTokenExpiration = TimeSpan.FromSeconds(86400);
});
builder.Services.AddAuthorization();

// add DI for services
builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddScoped<IAppService, AppService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<ICoQuanDonViService, CoQuanDonViService>();
builder.Services.AddScoped<IChiNhanhVanPhongService, ChiNhanhVanPhongService>();
builder.Services.AddScoped<IPhongBanService, PhongBanService>();
builder.Services.AddScoped<IAFeedbackService, FeedbackService>();
builder.Services.AddScoped<ITokenExpiredService, TokenExpiredService>();
builder.Services.AddScoped<IPerApiEndpointService, PerApiEndpointService>();
builder.Services.AddScoped<IFunctionService, FunctionService>();
builder.Services.AddScoped<IScopeService, ScopeService>();
builder.Services.AddScoped<IGroupFunctionService, GroupFunctionService>();
builder.Services.AddScoped<IPerRoleService, PerRoleService>();

ServiceRegister.AddLogic(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Enable CORS
app.UseCors("AllowAllOrigins");

app.UseMiddleware<HeaderExtractorMiddleWare>();
app.UseMiddleware<TokenCheckMiddleware>();

// get the section from appsettings.json
var isEnabled = app.Configuration.GetSection("EnableSecureMiddleware").Get<bool>();

if (isEnabled)
{
    app.UseMiddleware<PermissionCheckMiddleware>();
}

app.UseStaticFiles();
app.UseHttpsRedirection();

app.MapGroup("/api/auth")
    .CustomMapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers();

app.Run();
