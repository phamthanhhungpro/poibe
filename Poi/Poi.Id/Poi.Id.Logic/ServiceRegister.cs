using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Poi.Id.Logic
{
    public static class ServiceRegister
    {
        public static void AddLogic(IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
