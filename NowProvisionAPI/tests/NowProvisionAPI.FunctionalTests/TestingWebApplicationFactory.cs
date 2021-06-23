
namespace NowProvisionAPI.FunctionalTests
{
    using NowProvisionAPI.Infrastructure.Contexts;
    using NowProvisionAPI.WebApi;
    using WebMotions.Fake.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class TestingWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("FunctionalTesting");

            builder.ConfigureServices(services =>
            {
                // add authentication using a fake jwt bearer
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = FakeJwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = FakeJwtBearerDefaults.AuthenticationScheme;
                }).AddFakeJwtBearer();

                // Create a new service provider.
                var provider = services.BuildServiceProvider();

                // Add a database context (NowProvisionApiDbContext) using an in-memory database for testing.
                services.AddDbContext<NowProvisionApiDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseInternalServiceProvider(provider);
                });

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database context (NowProvisionApiDbContext).
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<NowProvisionApiDbContext>();

                    // Ensure the database is created.
                    db.Database.EnsureCreated();
                }
            });
        }
    }
}