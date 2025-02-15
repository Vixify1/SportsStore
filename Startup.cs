using Microsoft.EntityFrameworkCore;
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
                .AddJsonFile($"appsettings.Development.json", optional: true)
#else
                .AddJsonFile($"appsettings.Production.json", optional : true)
#endif
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDBContext>(options =>
            options.UseSqlServer(
                Configuration["ConnectionStrings:SportsStoreDB"]));
            services.AddTransient<IProductRepository, FakeProductRepository>();
            services.AddControllersWithViews();
            services.Configure<StoreSettings>(Configuration.GetSection("StoreSettings"));
            services.AddMvc(r => r.EnableEndpointRouting = false);
            services.AddMemoryCache();
            services.AddSession();
        }
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseExceptionHandler("/Home/Error");
            //app.UseSerilogRequestLogging();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc(routes =>
            {
                //routes.MapRoute(name: "pagination",
                //template: "Products/Page{productPage}",
                //defaults: new { Controller = "Home", action = "List" });

                routes.MapRoute(
                    name: null,
                template: "{category}/Page{productPage:int}",
                defaults: new { Controller = "Home", action = "List" });

                routes.MapRoute(
                    name: null,
                template: "Page",
                defaults: new { Controller = "Home", action = "List", productPage = 1 });

                routes.MapRoute(
                name: null,
                template: "{category}",
                defaults: new { Controller = "Home", action = "List", productPage = 1 });

                routes.MapRoute(
               name: null,
               template: "",
               defaults: new { Controller = "Home", action = "List", productPage = 1 });

                routes.MapRoute(
                    name: null,
                    template: "{controller}/{action}/{id?}");
            });
        }
    }
}
