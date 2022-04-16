using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fe_MaleShop.WebUI.Models.DataContexts;
using Fe_MaleShop.WebUI.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Fe_MaleShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
   
    public class CategoriesController : Controller
    {
        readonly Fe_MaleShopDbContext db;
        public CategoriesController(Fe_MaleShopDbContext db)
        {
            this.db = db;
        }
        [Authorize(Policy = "admin.categories.index")]
        public async  Task <IActionResult> Index()
        {
            var data =await db.Categories
                .Include( c=> c.Children)
                .Where(b => b.DeletedDate == null )
                .ToListAsync();
            return View(data);
        }
 
        [Authorize(Policy = "admin.categories.create")]
        public async Task<IActionResult>Details(int id)
        {
            if (id < 1)
            {
                return NotFound();//404
            }
            var entity = await db.Categories.FirstOrDefaultAsync(b => b.Id == id && b.DeletedDate == null);

            if (entity == null)
            {
                return NotFound();//404
            }

            return View(entity);
        }
        [Authorize(Policy = "admin.categories.create")]
        public  IActionResult Create()
        {
            var categories = db.Categories.Where(c => c.DeletedDate == null).ToList();
            var selectList = new SelectList(categories, "Id", "Name");

            ViewBag.Categories = selectList;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category model)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(model);
               await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var categories = db.Categories.Where(c => c.DeletedDate == null).ToList();
            var selectList = new SelectList(categories, "Id", "Name",model.ParentId);

            ViewBag.Categories = selectList;

            return View(model);
        }
        [Authorize(Policy = "admin.categories.edit")]
        public async Task <IActionResult> Edit(int id)
        {
            if (id <1)
            {
                return NotFound();//404
            }
            var entity = await db.Categories.FirstOrDefaultAsync(b=>b.Id == id && b.DeletedDate == null);

            if (entity == null)
            {
                return NotFound();//404
            }
            var categories = db.Categories.Where(c => c.DeletedDate == null).ToList();
            var selectList = new SelectList(categories, "Id", "Name", entity.ParentId);
            return View(entity);
        }
        [HttpPost]
        [Authorize(Policy = "admin.categories.index")]
        public async Task<IActionResult> Edit([FromRoute]int id, Category model)
        {
            if (!ModelState.IsValid)
            {
                var categories = db.Categories.Where(c => c.DeletedDate == null).ToList();
                var selectList = new SelectList(categories, "Id", "Name", model.ParentId);
                return View(model);
            }

            if (id != model.Id || id < 1)
            {
                return BadRequest();
            }
            var entity = await db.Categories.FirstOrDefaultAsync(b => b.Id == id && b.DeletedDate == null);

            if (entity == null)
            {
                return NotFound();//404
            }
            entity.Name = model.Name;
            entity.Description = model.Description;

            //db.Categories.Update(entity)
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            
        }
        [HttpPost]
        [Authorize(Policy = "admin.categories.delete")]
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
            var entity = await db.Categories.FirstOrDefaultAsync(b => b.Id == id && b.DeletedDate == null);

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
