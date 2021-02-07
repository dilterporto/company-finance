using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Finance.Persistence
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection SetupPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            // register db context
            services.AddDbContext<FinanceDbContext>(o =>
                o.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<DbContext, FinanceDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            return services;
        }
    }
}