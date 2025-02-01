using System;
using BankingApp.Infrastructure.Data;
using BankingApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BankingApp.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using BankingApp.Infrastructure.Services;


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



            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IPasswordHasher,BcryptPasswordHasher>();


            return services;
        }
    }
}
