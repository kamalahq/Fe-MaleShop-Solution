using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fe_MaleShop.WebUI.Models.DataContexts;
using Fe_MaleShop.WebUI.Models.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Fe_MaleShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
   

    public class BrandsController : Controller
    {
        readonly Fe_MaleShopDbContext db;
        public BrandsController(Fe_MaleShopDbContext db)
        {
            this.db = db;
        }
        [Authorize(Policy = "admin.brands.index")]
        public async  Task <IActionResult> Index()
        {
            var data =await db.Brands
                .Where(b => b.DeletedDate == null)
                .ToListAsync();

            return View(data);
        }
        [Authorize(Policy = "admin.brands.details")]
        public async Task<IActionResult>Details(int id)
        {
            if (id < 1)
            {
                return NotFound();//404
            }
            var entity = await db.Brands.FirstOrDefaultAsync(b => b.Id == id && b.DeletedDate == null);
            
            if (entity == null)
            {
                return NotFound();//404
            }

            return View(entity);
        }
        
        [Authorize(Policy = "admin.brands.create")]
        public  IActionResult Create()
        {
           
            return View();
        }
        [HttpPost]
        [Authorize(Policy = "admin.brands.create")]
        public async Task<IActionResult> Create(Brand model)
        {
            if (ModelState.IsValid)
            {
                db.Brands.Add(model);
               await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
            return View(model);
        }
        [Authorize(Policy = "admin.brands.edit")]
        public async Task <IActionResult> Edit(int id)
        {
            if (id <1)
            {
                return NotFound();//404
            }
            var entity = await db.Brands.FirstOrDefaultAsync(b=>b.Id == id && b.DeletedDate == null);

            if (entity == null)
            {
                return NotFound();//404
            }

            return View(entity);
        }
        [HttpPost]
        [Authorize(Policy = "admin.brands.edit")]
        public async Task<IActionResult> Edit([FromRoute]int id,Brand model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (id!= model.Id || id < 1)
            {
                return BadRequest();
            }
            var entity = await db.Brands.FirstOrDefaultAsync(b => b.Id == id && b.DeletedDate == null);

            if (entity == null)
            {
                return NotFound();//404
            }
            entity.Name = model.Name;
            entity.Description = model.Description;

            //db.Brands.Update(entity)
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            
        }
        [HttpPost]
        [Authorize(Policy = "admin.brands.edit")]
        public async Task<IActionResult> Delete(int id)
        {
          //  throw new Exception("erorrrrr");
            if (id < 1)
            {
                return Json(new
                {
                    error=true,
                    message = "Məlumat tapılmadı"
                });
            }
            var entity = await db.Brands.FirstOrDefaultAsync(b => b.Id == id && b.DeletedDate == null);

            if (entity == null)
                return Json(new
                {
                    error = true,
                    message = "Məlumat tapılmadı"
                });
            entity.DeletedDate = DateTime.UtcNow.AddHours(4);
           await db.SaveChangesAsync();

            return Json(new
            {
                error = false,
                message = "Məlumat silindi"
            });
        }
    }
}
