using System.Data;
using System.Data.Common;
using Autofac;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Facade.Contracts;
using ProductManagement.Facade.Query;
using ProductManagement.Facade.Service;
using ProductManagement.Persistence;

namespace ProductManagement.Bootstrap
{
    internal class ProductManagementModule : Module
    {
        private readonly string _connectionString;

        public ProductManagementModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ProductFacadeService).Assembly)
                .Where(a => typeof(IFacadeService).IsAssignableFrom(a))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(ProductFacadeQuery).Assembly)
                .Where(a => typeof(IFacadeService).IsAssignableFrom(a))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.Register<DbConnection>(z =>
            {
                var connection = new SqlConnection(_connectionString);
                connection.Open();
                return connection;
            }).InstancePerLifetimeScope().As<IDbConnection>().OnRelease(a => a.Close());


            builder.Register<ProductContext>(s =>
            {
                var options = new DbContextOptionsBuilder<ProductContext>()
                    .UseSqlServer(_connectionString)
                    .Options;
                var context = new ProductContext(options);
                return context;
            }).InstancePerLifetimeScope().OnRelease(a => a.Dispose());
        }
    }
}
