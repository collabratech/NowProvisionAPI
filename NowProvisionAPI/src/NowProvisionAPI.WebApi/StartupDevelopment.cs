namespace NowProvisionAPI.WebApi
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using NowProvisionAPI.Infrastructure;
    using NowProvisionAPI.Infrastructure.Seeders;
    using NowProvisionAPI.Infrastructure.Contexts;
    using NowProvisionAPI.WebApi.Extensions.Services;
    using NowProvisionAPI.WebApi.Extensions.Application;
    using Serilog;

    public class StartupDevelopment
    {
        public IConfiguration _config { get; }
        public IWebHostEnvironment _env { get; }

        public StartupDevelopment(IConfiguration configuration, IWebHostEnvironment env)
        {
            _config = configuration;
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCorsService("NowProvisionAPICorsPolicy");
            services.AddInfrastructure(_config, _env);
            services.AddMassTransitServices(_config);
            services.AddControllers()
                .AddNewtonsoftJson();
            services.AddApiVersioningExtension();
            services.AddWebApiServices();
            services.AddHealthChecks();

            // Dynamic Services
            services.AddSwaggerExtension(_config);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            // Entity Context - Do Not Delete

                using (var context = app.ApplicationServices.GetService<NowProvisionApiDbContext>())
                {
                    context.Database.EnsureCreated();

                    // NowProvisionApiDbContext Seeders

                    NowProvSeeder.SeedSampleNowProvData(app.ApplicationServices.GetService<NowProvisionApiDbContext>());
                    PropertySeeder.SeedSamplePropertyData(app.ApplicationServices.GetService<NowProvisionApiDbContext>());
                    AgentSeeder.SeedSampleAgentData(app.ApplicationServices.GetService<NowProvisionApiDbContext>());                    OfficeSeeder.SeedSampleOfficeData(app.ApplicationServices.GetService<NowProvisionApiDbContext>());
                }


            app.UseCors("NowProvisionAPICorsPolicy");

            app.UseSerilogRequestLogging();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseErrorHandlingMiddleware();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/api/health");
                endpoints.MapControllers();
            });

            // Dynamic App
            app.UseSwaggerExtension(_config);
        }
    }
}
