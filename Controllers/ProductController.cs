using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NextwoIdentity.Data;
using NextwoIdentity.Models;

namespace NextwoIdentity.Controllers
{
    [Authorize]

    public class ProductController : Controller
    {

     
        private NextwoDbContext db;
        private RoleManager<IdentityRole> roleManager;


        public ProductController( NextwoDbContext _db, RoleManager<IdentityRole> _roleManager)
        {
            this.db = _db;
            this.roleManager = _roleManager;
        }
        public IActionResult AllProduct()
        {
            return View(db.Products.Include(x => x.Category));
        }
        [Authorize(Roles = "Admin")]
        public IActionResult CreateProduct() {
            ViewBag.allCateg = new SelectList(db.Categorys, "CategoryId", "CategoryName");

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("AllProduct");
            }
            return View(product);
        }


      
        public ActionResult ALLCategory() {

                return View(db.Categorys);

        }
        [Authorize(Roles = "Admin")]

        public ActionResult CreateCategory() {

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task <ActionResult> CreateCategory(Category category)
        {
            

            if (ModelState.IsValid)
            {
                db.Categorys.Add(category);
                await db.SaveChangesAsync();
                return RedirectToAction("ALLCategory");
            }
            return View(category);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("AllProduct");
            }
            var data = db.Products.Find(id);
            if (data == null)
            {
                return RedirectToAction("AllProduct");
            }
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Update(product);
                db.SaveChanges();
                return RedirectToAction("AllProduct");
            }
            return View(product);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("AllProduct");
            }
            var data = db.Products.Find(id);
            if (data == null)
            {
                return RedirectToAction("AllProduct");
            }
            return View(data);
        }
        [HttpPost]
        public IActionResult Delete(Product product)
        {
            var data = db.Products.Find(product.ProductId);
            if (data == null)
            {
                return RedirectToAction("AllProduct");
            }
            db.Products.Remove(data);
            db.SaveChanges();
            return RedirectToAction("AllProduct");
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("AllProduct");
            }
            var data = db.Products.Find(id);
            if (data == null)
            {
                return RedirectToAction("AllProduct");
            }
            return View(data);
        }
    }
}
