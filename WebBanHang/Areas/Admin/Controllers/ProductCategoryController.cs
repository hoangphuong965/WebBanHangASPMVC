using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
using WebBanHang.Models.EF;

namespace WebBanHang.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductCategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/ProductCategory
        public ActionResult Index()
        {
            var items = db.ProductCategories.ToList();
            return View(items);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ProductCategory model)
        {
            var alias = db.ProductCategories.Where(temp => temp.Alias == model.Alias).FirstOrDefault();
            var title = db.ProductCategories.Where(temp => temp.Title == model.Title).FirstOrDefault();
            if(title != null)
            {
                ViewBag.checkTitle = "Ten danh muc da ton tai";
                ModelState.AddModelError("","");
            }

            if(alias != null)
            {
                ViewBag.checkAlias = "Ten danh muc alias da ton tai";
                ModelState.AddModelError("","");
            }
            
            
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifierDate = DateTime.Now;
                db.ProductCategories.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}