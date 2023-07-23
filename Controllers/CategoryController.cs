using MachineTest.Data;
using MachineTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.InteropServices;

namespace MachineTest.Controllers
{
    public class CategoryController : Controller
    {
        private readonly Applicationcontext context;

        public CategoryController(Applicationcontext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()

        {
            var data = await context.categories.ToListAsync();

            return View(data);
        }

        public async Task<IActionResult> addCategory(int? id)
        {
            category category = new category();
            if(id != null && id != 0)
            {
                category = await context.categories.FindAsync(id);
            }
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> addCategory(category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            else
            {
                if (category.categoryid == 0)
                {
                    await context.categories.AddAsync(category);
                    
                    TempData["succes"] = "Category Has been created";
                }
                else
                {
                    context.categories.Update(category);
                    TempData["succes"] = "Category Has been Updated";
                }
                await context.SaveChangesAsync();

            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)//Delete
        {
            if(id != 0)
            {
                bool status = context.products.Any(x=>x.categoryid== id);
                if (status)
                {
                    TempData["Warning"] = "Category is taken by another product, can't delete";
                }
                else
                {
                    var cat =  await context.categories.FindAsync(id);
                    if (cat == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        context.categories.Remove(cat);
                        await context.SaveChangesAsync();
                        TempData["succes"] = "Category has been deleted";
                    }
                    
                }
            }
            else
            {
                return BadRequest();
            }
            return RedirectToAction("Index");
        }
    }
}

