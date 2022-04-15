using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fe_MaleShop.WebUI.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Fe_MaleShop.WebUI.Models.Entities.Membership;

namespace Fe_MaleShop.WebUI.Models.DataContexts
{
    public class Fe_MaleShopDbContext : IdentityDbContext<Fe_MaleUser, Fe_MaleRole,int,Fe_MaleUserClaim, Fe_MaleUserRole, Fe_MaleUserLogin, Fe_MaleRoleClaim, Fe_MaleUserToken>
    {

        public Fe_MaleShopDbContext(DbContextOptions options)
            :base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=Fe_Male;User Id=sa;Password=query;MultipleActiveResultSets=true;");
            }
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
      
        public DbSet<Category> Categories { get; set; }
        public DbSet<AppInfo> AppInfos { get; set; }
        public DbSet<ProductSize> Sizes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }



        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ProductImages>(e => {
        //        e.HasNoKey();
        //    });
        //    base.OnModelCreating(modelBuilder);
        //}


    }
}
