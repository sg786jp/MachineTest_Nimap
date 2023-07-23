using MachineTest.Data;
using MachineTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MachineTest.Controllers
{
    public class ProductController : Controller
    {
        private readonly Applicationcontext context;

        public ProductController(Applicationcontext context)
        {
            this.context = context;
        }
        public IActionResult Index(int pg = 1)
        {
            // Join
            var data = (from p in context.products
                        join c in context.categories
                        on p.categoryid equals c.categoryid
                        select new ViewModel
                        {
                            productid = p.productid,
                            productname = p.productname,
                            categoryid = p.categoryid,
                            categoryname = c.categoryname,
                        }).ToList();
            const int pageSize = 3;
            if (pg < 1)
                pg = 1;

            int recsCount = data.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var datas = data.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;



            return View(datas);
        }

        public async Task<IActionResult> addProduct()
        {
            ViewBag.category = await context.categories.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> addProduct(ViewModel prodcat)
        {
            ViewBag.category = await context.categories.ToListAsync();
            try
            {
                ModelState.Remove("categoryname");
                ModelState.Remove("category");

                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError(string.Empty, "Please Enter Valid Data!!");
                    return View(prodcat);
                }
                else
                {
                    var data = new product()
                    {
                        productname = prodcat.productname,
                        categoryid = prodcat.categoryid,
                    };
                    await context.products.AddAsync(data);
                    await context.SaveChangesAsync();
                    TempData["succes"] = "Record Has Been Inserted!";
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                else
                {
                    var data = await context.products.FindAsync(id);
                    if (data == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        context.products.Remove(data);
                        await context.SaveChangesAsync();
                        TempData["succes"] = "Record Has Been Deleted!";
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DetailsProduct(int id)
        {
            ViewModel productDetails = new ViewModel();
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                else
                {
                    productDetails = (from p in context.products.Where(p => p.productid == id)
                                      join c in context.categories
                                      on p.categoryid equals c.categoryid
                                      select new ViewModel
                                      {
                                          productid = p.productid,
                                          productname = p.productname,
                                          categoryid = p.categoryid,
                                          categoryname = c.categoryname,
                                      }).First();
                    if (productDetails == null)
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(productDetails);
        }

        public async Task<IActionResult> EditProduct(int id)
        {
            ViewBag.category = await context.categories.ToListAsync();
            ViewModel productDetails = new ViewModel();
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                else
                {
                    productDetails = (from p in context.products.Where(p => p.productid == id)
                                      join c in context.categories
                                      on p.categoryid equals c.categoryid
                                      select new ViewModel
                                      {
                                          productid = p.productid,
                                          productname = p.productname,
                                          categoryid = p.categoryid,
                                          categoryname = c.categoryname,
                                      }).First();
                    if (productDetails == null)
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(productDetails);
        }

        [HttpPost]

        public async Task<IActionResult> EditProduct(ViewModel prodcat)
        {
            ViewBag.category = await context.categories.ToListAsync();
            try
            {
                ModelState.Remove("categoryname");
                ModelState.Remove("category");

                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError(string.Empty, "Please Enter Valid Data!!");
                    return View(prodcat);
                }
                else
                {
                    var data = new product()
                    {
                        productid = prodcat.productid,
                        productname = prodcat.productname,
                        categoryid = prodcat.categoryid,
                    };
                    context.products.Update(data);
                    await context.SaveChangesAsync();
                    TempData["succes"] = "Record Has Been Updated!";
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return RedirectToAction("Index");
        }
    }
}



