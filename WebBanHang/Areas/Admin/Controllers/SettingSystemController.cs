﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
using WebBanHang.Models.EF;

namespace WebBanHang.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SettingSystemController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/SettingSystem
        public ActionResult Index()
        {
            var items = db.SystemSettings.ToList();
            return View(items);
        }

        public ActionResult Partial_Setting()
        {
            return PartialView();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSetting(SettingSystemViewModel req)
        {
            SystemSetting set = null;
            var checkTitle = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingTitle"));
            if (checkTitle == null || req == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingTitle";
                set.SettingValue = req.SettingTitle;
                db.SystemSettings.Add(set);
            }
            else
            {
                checkTitle.SettingValue = req.SettingTitle;
                ViewBag.SettingTitle = req.SettingTitle;
                db.Entry(checkTitle).State = EntityState.Modified;
            }
            //logo
            var checkLogo = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingLogo"));
            if (checkLogo == null || req == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingLogo";
                set.SettingValue = req.SettingLogo;
                db.SystemSettings.Add(set);
            }
            else
            {
                checkLogo.SettingValue = req.SettingLogo;
                db.Entry(checkLogo).State = System.Data.Entity.EntityState.Modified;
            }
            //Email
            var email = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingEmail"));
            if (email == null || req == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingEmail";
                set.SettingValue = req.SettingEmail;
                db.SystemSettings.Add(set);
            }
            else
            {
                email.SettingValue = req.SettingEmail;
                db.Entry(email).State = System.Data.Entity.EntityState.Modified;
            }
            //Hotline
            var Hotline = db.SystemSettings.FirstOrDefault(x => x.SettingKey.Contains("SettingHotline"));
            if (Hotline == null || req == null)
            {
                set = new SystemSetting();
                set.SettingKey = "SettingHotline";
                set.SettingValue = req.SettingHotline;
                db.SystemSettings.Add(set);
            }
            else
            {
                Hotline.SettingValue = req.SettingHotline;
                db.Entry(Hotline).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}