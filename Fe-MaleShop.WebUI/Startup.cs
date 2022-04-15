using Fe_MaleShop.WebUI.AppCode.Extensions;
using Fe_MaleShop.WebUI.Models.DataContexts;
using Fe_MaleShop.WebUI.Models.Entities.Membership;
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
            string myKey = "Female";

            string plainText = "test";

            //string hashedText = plainText.ToMd5();
            string chiperText = plainText.Encrypt(myKey);  //kFVbS1CtCkE
            string MyplainText = chiperText.Decrypt(myKey);
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
           services.AddIdentity<Fe_MaleUser, Fe_MaleRole>()
            .AddEntityFrameworkStores<Fe_MaleShopDbContext>();
        }

        //.AddEntityFrameworkStores<Fe_MaleShopDbContext>();

        //services.AddScoped<SignInManager<Fe_MaleUser>>();
        //services.AddScoped<UserManager<Fe_MaleUser>>();
        //services.AddScoped<RoleManager<Fe_MaleRole>>();

        //services.AddAuthentication();
        //services.AddAuthorization(cfg => {

        //    foreach (var claimName in Program.policies)
        //    {
        //        cfg.AddPolicy(claimName, p =>
        //        {
        //            p.RequireAssertion(a =>
        //            {
        //                return a.User.HasClaim(claimName,"2")
        //                || a.User.IsInRole("SuperAdmin");
        //            });
        //           // p.RequireClaim(claimName, "1");
        //        });
        //    }

        //    });

        //services.ConfigureApplicationCookie(cfg =>
        //{
        //    cfg.Cookie.Name = "riode";
        //    cfg.Cookie.HttpOnly = true;
        //    cfg.ExpireTimeSpan = new TimeSpan(0, 5, 0);
        //    cfg.LoginPath = "/signin.html";
        //    cfg.AccessDeniedPath = "/accessdenied.html";
        //});

        //services.Configure<IdentityOptions>(cfg =>
        //{
        //    // cfg.User.AllowedUserNameCharacters = "abcde";
        //    cfg.User.RequireUniqueEmail = true;
        //    cfg.Password.RequireDigit = false;
        //    cfg.Password.RequiredLength = 3;
        //    cfg.Password.RequiredUniqueChars = 1;
        //    cfg.Password.RequireLowercase = false;
        //    cfg.Password.RequireUppercase = false;
        //    cfg.Password.RequireNonAlphanumeric = false;

        //    cfg.Lockout.MaxFailedAccessAttempts = 3;
        //    cfg.Lockout.DefaultLockoutTimeSpan = new TimeSpan(0, 5, 0);

        //});


        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env)


        //    if (env.IsDevelopment())
        //{
        //    app.UseDeveloperExceptionPage();
        //}
        //    //app.UseDeveloperExceptionPage();
        //    app.UseStaticFiles();
        //            app.UseRouting();
        //app.SeedMembership();



        //app.UseAuthentication();

        //app.UseAuthorization();
        //app.UseEndpoints(cfg =>
        //            {

        //                cfg.MapAreaControllerRoute(
        //              name: "Areas",
        //              areaName: "Admin",
        //               pattern: "admin/{controller=Dashboard}/{action=Index}/{id?}"
        //             );





        //                cfg.MapControllerRoute(
        //                   name: "default",
        //                   pattern: "{controller}/{action}/{id?}",
        //                   defaults: new
        //                   {
        //                       controller = "home",
        //                       action = "Index"
        //                   });


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
