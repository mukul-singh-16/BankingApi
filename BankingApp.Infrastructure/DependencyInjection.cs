using System;
using BankingApp.Infrastructure.Data;
using BankingApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BankingApp.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data.Common;


namespace BankingApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services , IConfiguration configuration)
        {
            
            services.AddDbContext<AppDbContext>(options =>
            {
               options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            // services.AddDbContext<AppDbContext>(options =>
            // {
            //     options.UseNpgsql(
            //         configuration.GetConnectionString("DefaultConnection"),
            //         b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName) // Specify the assembly containing the DbContext
            //     );
            // });

            services.AddScoped<IUserRepository, UserRepository>(); // Register UserRepository
            services.AddScoped<ITransactionRepository, TransactionRepository>(); // Register TransactionRepository

            return services;
        }
    }
}
