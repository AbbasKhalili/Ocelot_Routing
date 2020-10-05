using Autofac;

namespace ProductManagement.Bootstrap
{
    public static class RegisterProductModule
    {
        public static void AddModule(this ContainerBuilder builder, string connectionString)
        {
            builder.RegisterModule(new ProductManagementModule(connectionString));
        }
    }
}