using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
using WebBanHang.Models.EF;
using X.PagedList;

namespace WebBanHang.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/News
        public ActionResult Index(int? page, string find)
        {

            IEnumerable<News> news = db.News.OrderByDescending(x => x.Id);
            if (!string.IsNullOrEmpty(find))
            {
                news = news.Where(x => x.Alias.Contains(find) || x.Title.Contains(find));
            }
            var pageSize = 10;
            if (page == null) page = 1;
            var pageNumber = page.HasValue ? Convert.ToInt32(page) : 1;
            news = news.ToPagedList(pageNumber, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(news);
        }

        public ActionResult Add()
        {
            ViewBag.Categories = db.Categories.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(News news)
        {
            if (ModelState.IsValid)
            {
                news.CreatedDate = DateTime.Now;
                news.ModifierDate = DateTime.Now;
                news.Alias = WebBanHang.Models.Common.Filter.FilterChar(news.Title);
                db.Entry(news).State = System.Data.Entity.EntityState.Modified;
                db.News.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categories = db.Categories.ToList();
            return View(news);
        }

        public ActionResult Edit(int id)
        {
            var item = db.News.Find(id);
            ViewBag.Categories = db.Categories.ToList();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(News news)
        {
            if (ModelState.IsValid)
            {
                db.News.Attach(news);
                news.ModifierDate = DateTime.Now;
                news.Alias = WebBanHang.Models.Common.Filter.FilterChar(news.Title);
                db.Entry(news).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.News.Find(id);
            if (item != null)
            {
                db.News.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = db.News.Find(id);
            if(item != null)
            {
                item.IsActive = !item.IsActive;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, isAcive = item.IsActive });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult DeleteAll(int[] id)
        {
            if(id != null)
            {
                db.News.RemoveRange(db.News.Where(temp => id.Contains(temp.Id)));
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}