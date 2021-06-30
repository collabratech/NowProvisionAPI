namespace NowProvisionAPI.WebApi.Extensions.Services
{
    using AutoMapper;
    using FluentValidation.AspNetCore;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Reflection;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.Extensions.Hosting;
	using System.Linq;
	using Microsoft.IdentityModel.Tokens;
	using Microsoft.AspNetCore.Authentication.JwtBearer;

	public static class SwaggerServiceExtension
    {
		public static void AddSwaggerExtension(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "Now Provision API",
					Version = "v1",
					Description = "Manage NOW Platform Provision.",
					License = new OpenApiLicense()
					{
						Url = new Uri("https://www.collabratechnology.com/collabra-api-license"),
						Name = "Proprietary License"
					},
					TermsOfService = new Uri("https://www.collabratechnology.com/terms-of-service/"),
					Contact = new OpenApiContact()
					{
						Url = new Uri("https://www.collabratechnology.com/#contact"),
						Name = "Contact Us"
					}
				});
				c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
				//c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{typeof(OrderingMongoDbContext).Assembly.GetName().Name}.xml"));

				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "Please enter JWT with Bearer into field",
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey,
					Flows = new OpenApiOAuthFlows
					{
						AuthorizationCode = new OpenApiOAuthFlow
						{
							Scopes = new Dictionary<string, string>
								{
										{ "NowProv.read","CanReadNowProv" },
										{ "NowProv.add","CanAddNowProv" },
										{ "NowProv.delete","CanDeleteNowProv" },
										{ "NowProv.update","CanUpdateNowProv" },
								}
						}
					}
				});
				c.AddSecurityRequirement(new OpenApiSecurityRequirement {
					{
						new OpenApiSecurityScheme
							{
								Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer"}
							},
						Array.Empty<string>()
					}
				});
			});
		}

		public static void AddApiVersioningExtension(this IServiceCollection services)
		{
			services.AddApiVersioning(config =>
			{
				// Default API Version
				config.DefaultApiVersion = new ApiVersion(1, 0);
				// use default version when version is not specified
				config.AssumeDefaultVersionWhenUnspecified = true;
				// Advertise the API versions supported for the particular endpoint
				config.ReportApiVersions = true;
			});
		}

		public static void AddCorsService(this IServiceCollection services, string policyName)
		{
			services.AddCors(options =>
			{
				options.AddPolicy(policyName,
					builder => builder.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader()
					.WithExposedHeaders("X-Pagination"));
			});
		}

		public static void AddWebApiServices(this IServiceCollection services)
		{
			services.AddMediatR(typeof(Startup));
			services.AddMvc()
				.AddFluentValidation(cfg => { cfg.RegisterValidatorsFromAssemblyContaining<Startup>(); });
			services.AddAutoMapper(Assembly.GetExecutingAssembly());

		}

		public static void AddJwtAuthorization(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
		{
			if (environment.IsEnvironment("FunctionalTesting")) // Are we sure we don't want to use JWT for testing? Seems it's all part of integration testing.
				return;

			var iss = configuration["JwtConfig:JwtIssuer"];
			var aud = configuration["JwtConfig:JwtAudience"];
			var keys = configuration["JwtConfig:JwtKey"]?.Split(',')
				.Select(key => new SymmetricSecurityKey(Convert.FromBase64String(key)));

			services
				.AddAuthentication(options =>
				{
					options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
					options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				.AddJwtBearer(cfg =>
				{
					cfg.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidIssuers = iss.Split(','),
						ValidAudience = aud,
						IssuerSigningKeys = keys,
						ValidateAudience = true,
						ValidateIssuer = true,
						ValidateIssuerSigningKey = true,
						ValidateLifetime = false,
						RequireExpirationTime = false,
						ClockSkew = environment.IsEnvironment("test") ? TimeSpan.Zero : TimeSpan.FromMinutes(5) // Tests are running on the same machine and we want to verify this is working, so no skew.
					};
				});
		}
	}
}