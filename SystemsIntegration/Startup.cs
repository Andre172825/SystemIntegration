using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SystemsIntegration.Api.Configurations.appSetting;
using SystemsIntegration.Api.DataAccess.Contracts;
using SystemsIntegration.Api.DataAccess.Integracions;
using SystemsIntegration.Api.Services.Contracts;
using SystemsIntegration.Api.Services.Implementation;

namespace SystemsIntegration
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddControllers();
            services.AddScoped<IAppConfig, AppConfig>();

            services.AddScoped<IPedidoServices, PedidoServices>();
            services.AddScoped<IClienteServices, ClienteServices>();
            services.AddScoped<IServicioExternoServices, ServicioExternoServices>();

            services.AddScoped<IClienteContext, ClienteContext>();
            services.AddScoped<IPedidoContext, PedidoContext>();
            services.AddScoped<IServicioExternoContext, ServicioExternoContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
