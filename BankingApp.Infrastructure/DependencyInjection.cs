using System;
using BankingApp.Infrastructure.Data;
using BankingApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BankingApp.Core.Interfaces;


namespace BankingApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
        {
            
            services.AddDbContext<BankingAppContext>(options =>
            {
                options.UseNpgsql("Host=localhost;Port=5432;Database=BankingApp;Username=postgres;Password=1234");
            });

            services.AddScoped<IUserRepository, UserRepository>(); // Register UserRepository
            services.AddScoped<ITransactionRepository, TransactionRepository>(); // Register TransactionRepository

            return services;
        }
    }
}
