using Fe_MaleShop.WebUI.Models.DataContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fe_MaleShop.WebUI
{
    public class Startup
    {
        readonly IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddRouting(cfg =>
            {
                cfg.LowercaseUrls = true;
            });
            services.AddDbContext<Fe_MaleShopDbContext>(cfg =>
            {
                cfg.UseSqlServer(configuration.GetConnectionString("cString"));
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(cfg =>
            {

                cfg.MapAreaControllerRoute(
              name: "Areas",
              areaName: "Admin",
               pattern: "admin/{controller=Dashboard}/{action=Index}/{id?}"
             );





                cfg.MapControllerRoute(
                   name: "default",
                   pattern: "{controller}/{action}/{id?}",
                   defaults: new
                   {
                       controller = "home",
                       action = "Index"
                   });




                //   cfg.MapAreaControllerRoute("adminArea",
                // areaName: "Admin",
                //pattern: "admin/{controller=Dashboard}/{action=index}/{id?}");

                //   cfg.MapControllerRoute(
                //   name: "default",
                //   pattern: "{controller=home}/{action=index}/{id?}");



            });
        }
    }
}
