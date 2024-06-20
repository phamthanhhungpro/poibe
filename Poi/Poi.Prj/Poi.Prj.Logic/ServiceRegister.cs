using Microsoft.Extensions.DependencyInjection;
using Poi.Prj.Logic.Interface;
using Poi.Prj.Logic.Service;
using System.Reflection;

namespace Poi.Prj.Logic
{
    public static class ServiceRegister
    {
        public static void AddLogic(IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<ILinhVucService, LinhVucService>();
            services.AddScoped<IToNhomService, ToNhomService>();
        }
    }
}
