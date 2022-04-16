using Fe_MaleShop.WebUI.Models.Entities.Membership;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fe_MaleShop.WebUI.Models.DataContexts
{
    static public class Fe_MaleShopDbSeed
    {
        static internal IApplicationBuilder SeedMembership(this IApplicationBuilder builder)
        {
            const string adminEmail = "kamalagh@code.edu.az";
            const string adminPassword = "123";
            const string  superAdminRoleName = "SuperAdmin";
            using (var scope = builder.ApplicationServices.CreateScope())
            {

               var db = scope.ServiceProvider.GetRequiredService<Fe_MaleShopDbContext>();
                db.Database.Migrate();

              var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Fe_MaleRole>>();
                var role =   roleManager.FindByNameAsync(superAdminRoleName).Result;

                if (role == null)
                {
                    role = new Fe_MaleRole
                    {
                       Name = superAdminRoleName
                    };
                    roleManager.CreateAsync(role).Wait();
                }
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Fe_MaleUser>>();
             var adminUser =  userManager.FindByEmailAsync(adminEmail).Result;
                if (adminUser == null)
                {
                    adminUser = new Fe_MaleUser
                    {
                     Email = adminEmail,
                     UserName = adminEmail,
                     EmailConfirmed = true

                    };

                 var userResult =  userManager.CreateAsync(adminUser, adminPassword).Result;
                    if (userResult.Succeeded)
                    {
                        userManager.AddToRoleAsync(adminUser, superAdminRoleName).Wait();
                    }
                }  
            }
            return builder;
        }
    }
}


