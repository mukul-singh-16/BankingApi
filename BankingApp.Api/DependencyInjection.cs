using BankingApp.Application;
using BankingApp.Infrastructure;

namespace BankingApp.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddApplicationDI().AddInfrastructureDI();
            return services;
        }
    }
}
