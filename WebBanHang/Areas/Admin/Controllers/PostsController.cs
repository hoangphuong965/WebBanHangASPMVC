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
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Posts
        public ActionResult Index(int? page, string find)
        {
            IEnumerable<Post> posts = db.Posts.OrderByDescending(x => x.Id);
            if (!string.IsNullOrEmpty(find))
            {
                posts = posts.Where(x => x.Alias.Contains(find) || x.Title.Contains(find));
            }
            var pageSize = 10;
            if (page == null) page = 1;
            var pageNumber = page.HasValue ? Convert.ToInt32(page) : 1;
            posts = posts.ToPagedList(pageNumber, pageSize);
            return View(posts);
        }
        public ActionResult Add()
        {
            ViewBag.Categories = db.Categories.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Post post)
        {
            if (ModelState.IsValid)
            {
                post.CreatedDate = DateTime.Now;
                post.ModifierDate = DateTime.Now;
                post.Alias = WebBanHang.Models.Common.Filter.FilterChar(post.Title);
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categories = db.Categories.ToList();
            return View(post);
        }

        public ActionResult Edit(int id)
        {
            Post post = db.Posts.Where(temp => temp.Id == id).FirstOrDefault();
            ViewBag.Categories = db.Categories.ToList();
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Attach(post);
                post.ModifierDate = DateTime.Now;
                post.Alias = WebBanHang.Models.Common.Filter.FilterChar(post.Title);
                db.Entry(post).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categories = db.Categories.ToList();
            return View(post);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.Posts.Find(id);
            if (item != null)
            {
                db.Posts.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = db.Posts.Find(id);
            if (item != null)
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
            if (id != null)
            {
                db.Posts.RemoveRange(db.Posts.Where(temp => id.Contains(temp.Id)));
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}