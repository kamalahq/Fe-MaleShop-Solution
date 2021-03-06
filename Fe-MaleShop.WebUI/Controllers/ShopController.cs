using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fe_MaleShop.WebUI.Models.DataContexts;
using Fe_MaleShop.WebUI.Models.Entities;
using Fe_MaleShop.WebUI.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace Fe_MaleShop.WebUI.Controllers
{
    public class ShopController : Controller
    {
        readonly Fe_MaleShopDbContext db;
        public ShopController(Fe_MaleShopDbContext db)
        {
            this.db = db;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            

            ShopFilterViewModel vm = new ShopFilterViewModel();

            vm.Brands = db.Brands

                .Where(b => b.DeletedByUserId == null)
                .ToList();

            vm.Colors = db.Colors
                .Where(b => b.DeletedByUserId == null)
                .ToList();
            return View(vm);

            vm.Sizes = db.Sizes
                .Where(b => b.DeletedByUserId == null)
                .ToList();
            return View(vm);

            vm.Categories = db.Categories
                .Where(b => b.DeletedByUserId == null)
                .ToList();
            return View(vm);

        }
        //public IActionResult Details(int id)
        //{
        //    Fe_MaleShopDbContext db = new Fe_MaleShopDbContext();

        //    return View();
        //}

    }
    
}
