using Microsoft.Extensions.DependencyInjection;
using Poi.Hrm.Logic.Interface;
using Poi.Hrm.Logic.Service;
using System.Reflection;

namespace Poi.Hrm.Logic
{
    public static class ServiceRegister
    {
        public static void AddLogic(IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IHoSoNhanSuService, HoSoNhanSuService>();
            services.AddScoped<IVaiTroService, VaiTroService>();
            services.AddScoped<IViTriCongViecService, ViTriCongViecService>();
        }
    }
}
