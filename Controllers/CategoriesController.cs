using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NextwoIdentity.Data;
using NextwoIdentity.Models;

namespace NextwoIdentity.Controllers
{
    public class CategoriesController : Controller
    {
        private NextwoDbContext db;
        private RoleManager<IdentityRole> roleManager;


        public CategoriesController(NextwoDbContext _db, RoleManager<IdentityRole> _roleManager)
        {
            this.db = _db;
            this.roleManager = _roleManager;
        }
        public IActionResult Index()
        {

            return View(db.Categorys);
        }

        public IActionResult CreateCategory()
        {
            ViewBag.allCateg = new SelectList(db.Categorys, "CategoryId", "CategoryName");
            return View();
        }


        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                var test = db.Categorys.FirstOrDefault(c => c.CategoryName! == category.CategoryName!);
                if (test == null)
                {
                    db.Categorys.Add(category);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.check = "Exists";
                }
            }
            return View(category);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var data = db.Categorys.Find(id);
            if (data == null)
            {
                return RedirectToAction("Index");
            }
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categorys.Update(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var data = db.Categorys.Find(id);
            if (data == null)
            {
                return RedirectToAction("Index");
            }
            return View(data);
        }
        [HttpPost]
        public IActionResult Delete(Category category)
        {
            var data = db.Categorys.Find(category.CategoryId);
            if (data == null)
            {
                return RedirectToAction("Index");
            }
            db.Categorys.Remove(data);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var data = db.Categorys.Find(id);
            if (data == null)
            {
                return RedirectToAction("Index");
            }
            return View(data);
        }

        //public IActionResult test(Category category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var test = db.Categorys.FirstOrDefault(c => c.CategoryName! == category.CategoryName!);
        //        if (test == null)
        //        {
        //            db.Categorys.Add(category);
        //            db.SaveChanges();
        //            return View(test);
        //        }
        //        else
        //        {
        //            ViewBag.check = "Exists";
        //        }
        //    }
        //    return View(category);
        //}
    }
}
