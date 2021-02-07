using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Finance.Setups
{
    public static class AutomapperExtensions
    {
        public static IServiceCollection SetupAutomapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}