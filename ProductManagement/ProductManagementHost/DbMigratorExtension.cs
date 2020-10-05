using System.Reflection;
using Autofac;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace ProductManagementHost
{
    public static class DbMigratorExtension
    {
        public static void AddFluentMigrator(this IServiceCollection services, string connectionString, Assembly assembly)
        {
            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        public static void RunMigration(this ILifetimeScope lifeTime)
        {
            using (var scope = lifeTime.BeginLifetimeScope())
            {
                UpdateDatabase(scope);
            }
        }

        private static void UpdateDatabase(ILifetimeScope lifeTime)
        {
            var runner = lifeTime.Resolve<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}