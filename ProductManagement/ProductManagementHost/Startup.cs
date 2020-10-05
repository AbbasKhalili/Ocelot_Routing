using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductManagement.Bootstrap;
using ProductManagement.Migration;

namespace ProductManagementHost
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public HostConfig HostConfig { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddControllersAsServices();

            HostConfig = new HostConfig();
            Configuration.Bind("HostConfig", HostConfig);
            services.AddSingleton(HostConfig);

            services.AddFluentMigrator(HostConfig.DbConnection, typeof(_0001_Products).Assembly);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.AddModule(HostConfig.DbConnection);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.ApplicationServices.GetAutofacRoot().RunMigration();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
