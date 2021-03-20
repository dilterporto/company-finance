using Company.Finance.Application.Statements.Commands;
using Company.Finance.Application.Statements.Parsers;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Finance.Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection SetupApplication(this IServiceCollection services)
        {
            // register parser
            services
                .AddTransient<IStreamToCommandParser<CreateStatementCommand>, 
                    OfxStatementStreamToCommandParser>();
            
            return services;
        }
    }
}