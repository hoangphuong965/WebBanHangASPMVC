using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Role
        public ActionResult Index()
        {
            var items = db.Roles.ToList();
            return View(items);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IdentityRole model)
        {
            var RoleExist = db.Roles.Any(x => x.Name == model.Name);
            if (RoleExist)
            {
                TempData["Message"] = "Role tồn tại";
                TempData["RoleName"] = model.Name;
                return RedirectToAction("Create");
            }
            if (model.Name == null)
            {
                TempData["Message"] = "Vui lòng nhập tên Role";
                return RedirectToAction("Create");
            }
            if (ModelState.IsValid)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                roleManager.Create(model);
                TempData["Message"] = "Thêm Role mới thành công";
                return RedirectToAction("index");
            }
            return View(model);
        }

        

        [HttpPost]
        public ActionResult Edit(IdentityRole model)
        {
            var RoleExist = db.Roles.Any(x => x.Name == model.Name);
            if (RoleExist)
            {
                return Json(new { success = false, msg = "Role tồn tại" });
            }
            if (ModelState.IsValid && model.Name != null)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                roleManager.Update(model);
                db.SaveChanges();
                var roleName = model.Name;
                return Json(new { success = true, RoleName = roleName });
            }
            return Json(new { success = false, msg = "khong duoc de role trong" });
        }

        public ActionResult GetNameRole(string roleid)
        {
            var role = db.Roles.Find(roleid);
            if(role != null)
            {
                var roleName = role.Name;
                return Json(new { success = true, RoleName= roleName }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(string id)
        {
            var role = db.Roles.Find(id);
            if (role != null)
            {
                var roleName = db.Roles.Remove(role);
                db.SaveChanges();
                return Json(new { success = true, RoleName = roleName }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }
    }
}