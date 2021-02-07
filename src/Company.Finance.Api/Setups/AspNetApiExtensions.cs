using System.Reflection;
using System.Text.Json.Serialization;
using Company.Finance.Apis.Statements.Validators;
using Company.Finance.Filters;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Company.Finance.Setups
{
    public static class AspNetApiExtensions
    {
        public static IServiceCollection SetupAspNetApi(this IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            services.AddMvc(options =>
                {
                    options.Filters.Add<ModelStateValidationFilter>();
                    options.Filters.Add<UnitOfWorkAttributeFilter>();

                })
                // setup fluent validation
                .AddFluentValidation(fv =>
                    fv.RegisterValidatorsFromAssemblyContaining<StatementsIngestionRequestValidator>());
            
            // setup MediatR
            services.AddMediatR(Assembly.GetExecutingAssembly());
            
            return services;
        }   
    }
}