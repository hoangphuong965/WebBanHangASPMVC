using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Product
        public ActionResult Index()
        {
            var products = db.Products.ToList();   
            return View(products);
        }

        public ActionResult Detail(string alias, int id)
        {
            var item = db.Products.Find(id);
            if(item != null)
            {
                db.Products.Attach(item);
                item.ViewCount = item.ViewCount + 1;
                db.Entry(item).Property(x => x.ViewCount).IsModified = true;
                db.SaveChanges();
            }
            return View(item);
        }

        public ActionResult ProductCategory(string Alias)
        {
            var ProductCategories = db.Products.Where(x => x.ProductCategory.Alias == Alias).ToList();
            ViewBag.AliasCategory = Alias;
            return View(ProductCategories);
        }
        public ActionResult Partial_ItemsByAlias()
        {
            var items = db.Products.Where(temp => temp.IsHome && temp.IsActive).Take(12).ToList();
            return PartialView(items);
        }

        public ActionResult Partial_ProductSales()
        {
            var items = db.Products.Where(temp => temp.IsFeature || temp.IsSale).Take(12).ToList();
            return PartialView(items);
        }
    }
}