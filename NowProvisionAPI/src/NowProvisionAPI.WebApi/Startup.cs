namespace NowProvisionAPI.WebApi
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using NowProvisionAPI.Infrastructure;
    using NowProvisionAPI.WebApi.Extensions.Services;
    using NowProvisionAPI.WebApi.Extensions.Application;
    using Serilog;
	using Serilog.AspNetCore;
	using System.Linq;
	using System.IdentityModel.Tokens.Jwt;
	using System;
	using Microsoft.Extensions.Hosting;
	using Hangfire;

	public class Startup
    {
        public IConfiguration _config { get; }
        public IWebHostEnvironment _env { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
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
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();

            // For elevated security, it is recommended to remove this middleware and set your server to only listen on https.
            // A slightly less secure option would be to redirect http to 400, 505, etc.
            app.UseHttpsRedirection();

            app.UseCors("NowProvisionAPICorsPolicy");

			app.UseSerilogRequestLogging(options => SetupRequestLogging(options, env.IsDevelopment()));
			app.UseRouting();

			app.UseHangfireDashboard();
			app.UseHangfireServer();

			app.UseDeveloperExceptionPage();
			//app.UseBrowserLink();

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
		private static void SetupRequestLogging(RequestLoggingOptions options, bool isDev)
		{
			options.EnrichDiagnosticContext = (diagCtx, httpCtx) =>
			{
				var request = httpCtx.Request;
				var response = httpCtx.Response;

				if (isDev)
				{
					// Only log the JWT authorization in development environment.
					var auth = request.Headers["Authorization"].FirstOrDefault();
					if (string.IsNullOrEmpty(auth))
						diagCtx.Set("Authorization", "Anonymous");
					else if (auth?.StartsWith("Bearer ", StringComparison.InvariantCultureIgnoreCase) ?? false)
						// Assuming all requests using bearer authorization use a JWT, so should be safe to put in logs.
						// Strip off "Bearer " so that the JWT is easier to copy from the log.
						diagCtx.Set("Authorization", auth["Bearer ".Length..]);
					else
						diagCtx.Set("Authorization", "SECRET");
				}

				// Log the authentication claims so they can be searched.
				var claims = httpCtx.User.Claims
					.Select(c =>
					{
						// We want to avoid using names like "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
						// The map should provide something a little more readable, even if it might not be what was sent in the JWT.
						var mappedName = JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.FirstOrDefault(x => x.Value == c.Type).Key;
						return new { Key = mappedName ?? c.Type, Value = c.Value };
					})
					.GroupBy(c => c.Key, (key, group) => new { ClaimName = key, ClaimValue = group.ToList() })
					.ToDictionary(c => c.ClaimName, c => c.ClaimValue);
				diagCtx.Set("Claims", claims);

				// Map the route parameters so they can be searched.
				// Each param is set individually so that values can be searched easily whether they are part of the route or query string.
				var routeVals = request.RouteValues
					.ToDictionary(c => c.Key, c => c.Value);
				diagCtx.Set("Route", routeVals);

				// Map the query string parameters so they can be searched.
				// Each param is set individually so that values can be searched easily whether they are part of the route or query string.
				var query = request.Query
					.ToDictionary(q => q.Key, q =>
					{
						if (q.Value.Count == 1)
							return q.Value.First();
						else
							return $"[{string.Join(", ", q.Value)}]";
					});
				diagCtx.Set("Query", query);

				diagCtx.Set("Request", new
				{
					request.Scheme,
					request.Host,
					request.ContentType,
					request.ContentLength,
				});

				diagCtx.Set("Response", new
				{
					response.ContentType,
					response.ContentLength,
					response.StatusCode
				});

				diagCtx.Set("IpAddress", httpCtx.Connection.RemoteIpAddress);
			};

		}
	}
}
