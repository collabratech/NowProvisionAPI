namespace NowProvisionAPI.IntegrationTests
{
    using NowProvisionAPI.Infrastructure.Contexts;
    using NowProvisionAPI.IntegrationTests.TestUtilities;
    using NowProvisionAPI.WebApi;
    using MediatR;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using NUnit.Framework;
    using Respawn;
    using System;
    using MassTransit.Testing;
    using MassTransit;
    using NowProvisionAPI.WebApi.Features.Consumers;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    [SetUpFixture]
    public class TestFixture
    {
        private static IConfigurationRoot _configuration;
        private static IWebHostEnvironment _env;
        private static IServiceScopeFactory _scopeFactory;
        private static Checkpoint _checkpoint;
        public static InMemoryTestHarness _harness;

        private string _dockerContainerId;
        private string _dockerSqlPort;

        [OneTimeSetUp]
        public async Task RunBeforeAnyTests()
        {
            (_dockerContainerId, _dockerSqlPort) = await DockerSqlDatabaseUtilities.EnsureDockerStartedAndGetContainerIdAndPortAsync();
            var dockerConnectionString = DockerSqlDatabaseUtilities.GetSqlConnectionString(_dockerSqlPort);

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddInMemoryCollection(new Dictionary<string, string>
                    {
                        { "UseInMemoryDatabase", "false" },
                        { "UseInMemoryBus", "true" },
                        { "ConnectionStrings:NowProvisionApiDbContext", dockerConnectionString }
                    })
                .AddEnvironmentVariables();

            _configuration = builder.Build();
            _env = Mock.Of<IWebHostEnvironment>();

            var startup = new Startup(_configuration, _env);

            var services = new ServiceCollection();

            services.AddLogging();

            startup.ConfigureServices(services);

            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            _checkpoint = new Checkpoint
            {
                TablesToIgnore = new[] { "__EFMigrationsHistory" },
            };

            EnsureDatabase();

            // MassTransit Setup -- Do Not Delete Comment
            services.AddMassTransitInMemoryTestHarness(cfg =>
            {
                // Consumer Registration -- Do Not Delete Comment

                cfg.AddConsumer<SenderOfAllReports>();
                cfg.AddConsumerTestHarness<SenderOfAllReports>();
            });
            _harness = services.BuildServiceProvider().GetRequiredService<InMemoryTestHarness>();
            await _harness.Start();
        }

        private static void EnsureDatabase()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<NowProvisionApiDbContext>();

            context.Database.Migrate();
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<ISender>();

            return await mediator.Send(request);
        }

        public static async Task ResetState()
        {
            await _checkpoint.Reset(_configuration.GetConnectionString("NowProvisionApiDbContext"));
        }

        public static async Task<TEntity> FindAsync<TEntity>(params object[] keyValues)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<NowProvisionApiDbContext>();

            return await context.FindAsync<TEntity>(keyValues);
        }

        public static async Task AddAsync<TEntity>(TEntity entity)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<NowProvisionApiDbContext>();

            context.Add(entity);

            await context.SaveChangesAsync();
        }

        public static async Task ExecuteScopeAsync(Func<IServiceProvider, Task> action)
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<NowProvisionApiDbContext>();

            try
            {
                //await dbContext.BeginTransactionAsync();

                await action(scope.ServiceProvider);

                //await dbContext.CommitTransactionAsync();
            }
            catch (Exception)
            {
                //dbContext.RollbackTransaction();
                throw;
            }
        }

        public static async Task<T> ExecuteScopeAsync<T>(Func<IServiceProvider, Task<T>> action)
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<NowProvisionApiDbContext>();

            try
            {
                //await dbContext.BeginTransactionAsync();

                var result = await action(scope.ServiceProvider);

                //await dbContext.CommitTransactionAsync();

                return result;
            }
            catch (Exception)
            {
                //dbContext.RollbackTransaction();
                throw;
            }
        }

        public static Task ExecuteDbContextAsync(Func<NowProvisionApiDbContext, Task> action)
            => ExecuteScopeAsync(sp => action(sp.GetService<NowProvisionApiDbContext>()));

        public static Task ExecuteDbContextAsync(Func<NowProvisionApiDbContext, ValueTask> action)
            => ExecuteScopeAsync(sp => action(sp.GetService<NowProvisionApiDbContext>()).AsTask());

        public static Task ExecuteDbContextAsync(Func<NowProvisionApiDbContext, IMediator, Task> action)
            => ExecuteScopeAsync(sp => action(sp.GetService<NowProvisionApiDbContext>(), sp.GetService<IMediator>()));

        public static Task<T> ExecuteDbContextAsync<T>(Func<NowProvisionApiDbContext, Task<T>> action)
            => ExecuteScopeAsync(sp => action(sp.GetService<NowProvisionApiDbContext>()));

        public static Task<T> ExecuteDbContextAsync<T>(Func<NowProvisionApiDbContext, ValueTask<T>> action)
            => ExecuteScopeAsync(sp => action(sp.GetService<NowProvisionApiDbContext>()).AsTask());

        public static Task<T> ExecuteDbContextAsync<T>(Func<NowProvisionApiDbContext, IMediator, Task<T>> action)
            => ExecuteScopeAsync(sp => action(sp.GetService<NowProvisionApiDbContext>(), sp.GetService<IMediator>()));

        public static Task<int> InsertAsync<T>(params T[] entities) where T : class
        {
            return ExecuteDbContextAsync(db =>
            {
                foreach (var entity in entities)
                {
                    db.Set<T>().Add(entity);
                }
                return db.SaveChangesAsync();
            });
        }

        // MassTransit Methods -- Do Not Delete Comment
        public static async Task PublishMessage<T>(object message)
            where T : class
        {
            await _harness.Bus.Publish<T>(message);
        }

        [OneTimeTearDown]
        public async Task RunAfterAnyTests()
        {
            // MassTransit Teardown -- Do Not Delete Comment
            await _harness.Stop();
        }
    }
}
