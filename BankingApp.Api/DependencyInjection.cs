using BankingApp.Application;
using BankingApp.Core.Interfaces;
using BankingApp.Infrastructure;
//using BankingApp.Infrastructure.Services;

namespace BankingApp.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services, IConfiguration configuration)
        {
              
            services.AddApplicationDI().AddInfrastructureDI(configuration);

            return services;
        }
    }
}
