namespace Ordering.Infrastructure
{
    using Ordering.Infrastructure.Contexts;
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
                services.AddDbContext<OrderingDbContext>(options =>
                    options.UseInMemoryDatabase($"OrderingDbContext"));
            }
            else
            {
                services.AddDbContext<OrderingDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("OrderingDbContext"),
                        builder => builder.MigrationsAssembly(typeof(OrderingDbContext).Assembly.FullName)));
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
                options.AddPolicy("CanReadOrder",
                    policy => policy.RequireClaim("scope", "order.read"));
                options.AddPolicy("CanAddOrder",
                    policy => policy.RequireClaim("scope", "order.add"));
                options.AddPolicy("CanDeleteOrder",
                    policy => policy.RequireClaim("scope", "order.delete"));
                options.AddPolicy("CanUpdateOrder",
                    policy => policy.RequireClaim("scope", "order.update"));
            });
        }
    }
}
