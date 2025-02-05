using Serilog;
using SportsStore.Models;
using System.Runtime.InteropServices;

namespace SportsStore
{
    public class Startup
    {

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
#if DEBUG
                .AddJsonFile($"appsettings.Production.json", optional: true)
#else
                .AddJsonFile($"appsettings,Development.json", optional : true)
#endif
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IProductRepository, FakeProductRepository>();
            services.AddControllersWithViews();
            services.Configure<StoreSettings>(Configuration.GetSection("StoreSettings"));
            services.AddMvc(r=>r.EnableEndpointRouting = false);

        }
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseExceptionHandler("/Home/Error");
            //app.UseSerilogRequestLogging();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "pagination",
                template: "Products/Page{productPage}",
                defaults: new { Controller = "Product", action = "List" });

                routes.MapRoute(
                    name: "Default",
                    template: "{controller=Product}/{action=List}/{id?}");
            });
        }
    }
}
