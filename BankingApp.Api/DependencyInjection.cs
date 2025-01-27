﻿using BankingApp.Application;
using BankingApp.Infrastructure;

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
