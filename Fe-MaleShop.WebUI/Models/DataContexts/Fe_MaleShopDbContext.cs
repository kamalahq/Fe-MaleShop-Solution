using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fe_MaleShop.WebUI.Models.Entities;

namespace Fe_MaleShop.WebUI.Models.DataContexts
{
    public class Fe_MaleShopDbContext : DbContext
    {

        public Fe_MaleShopDbContext(DbContextOptions options)
            :base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
      
        public DbSet<Category> Categories { get; set; }
        public DbSet<AppInfo> AppInfos { get; set; }
        public DbSet<ProductSize> Sizes { get; set; }
        
        

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ProductImages>(e => {
        //        e.HasNoKey();
        //    });
        //    base.OnModelCreating(modelBuilder);
        //}


    }
}
