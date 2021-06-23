namespace NowProvisionAPI.Infrastructure
{
    using NowProvisionAPI.Infrastructure.Contexts;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Sieve.Services;

    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            // DbContext -- Do Not Delete
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<NowProvisionApiDbContext>(options =>
                    options.UseInMemoryDatabase($"NowProvisionApiDbContext"));
            }
            else
            {
                services.AddDbContext<NowProvisionApiDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("NowProvisionApiDbContext"),
                        builder => builder.MigrationsAssembly(typeof(NowProvisionApiDbContext).Assembly.FullName)));
            }

            services.AddScoped<SieveProcessor>();

            // Auth -- Do Not Delete
            if(env.EnvironmentName != "FunctionalTesting")
            {
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.Authority = configuration["JwtSettings:Authority"];
                        options.Audience = configuration["JwtSettings:Audience"];
                    });
            }

            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanReadNowProv",
                    policy => policy.RequireClaim("scope", "NowProv.read"));
                options.AddPolicy("CanAddNowProv",
                    policy => policy.RequireClaim("scope", "NowProv.add"));
                options.AddPolicy("CanDeleteNowProv",
                    policy => policy.RequireClaim("scope", "NowProv.delete"));
                options.AddPolicy("CanUpdateNowProv",
                    policy => policy.RequireClaim("scope", "NowProv.update"));
            });
        }
    }
}
