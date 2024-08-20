using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Poi.Hrm.Logic;
using Poi.HRM.API.Middlewares;
using Poi.Id.InfraModel.DataAccess;
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

// Add Hangfire services.
//builder.Services.AddHangfire(config =>
//config.UsePostgreSqlStorage(c =>
//        c.UseNpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"))));

//builder.Services.AddHangfireServer();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<HrmDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
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
app.UseMiddleware<AppPermissionMiddleware>();

app.UseHttpsRedirection();

//app.UseAuthorization();
//app.UseHangfireDashboard();
// Schedule the job to run at 10 PM every day.
// RecurringJob.AddOrUpdate<SyncChamCongService>(job => job.SyncChamCong(), "0 21 * * 1-5");


app.MapControllers();

app.Run();
