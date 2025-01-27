using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BankingApp.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);
            var app = builder.Build();
            ConfigureMiddleware(app);
            app.Run();
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
           
            builder.Services.AddAppDI(builder.Configuration);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                builder.Configuration.Bind("JwtSettings", options);
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                builder.Configuration.Bind("CookieSettings", options);
            });
        }

        private static void ConfigureMiddleware(WebApplication app)
        {
            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

           
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}
