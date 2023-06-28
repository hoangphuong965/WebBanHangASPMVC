using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
using WebBanHang.Models.EF;
using X.PagedList;
using WebBanHang.Models.Common;
using Filter = WebBanHang.Models.Common.Filter;

namespace WebBanHang.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Products
        public ActionResult Index(int? page, string find)
        {
            IEnumerable<Product> products = db.Products.OrderByDescending(x => x.Id);
            var pageSize = 10;
            if (!string.IsNullOrEmpty(find))
            {
                products = products.Where(x => x.Title.Contains(find));
            }
            if (page == null) page = 1;
            var pageNumber = page.HasValue ? Convert.ToInt32(page) : 1;
            products = products.ToPagedList(pageNumber, pageSize);
            return View(products);
        }

        public ActionResult Add()
        {
            ViewBag.ProductCategory = db.ProductCategories.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Product model, List<string> Image, List<int> rDefault)
        {
            if (ModelState.IsValid)
            {
                if(Image != null && Image.Count > 0)
                {
                    for (int i = 0; i < Image.Count; i++)
                    {
                        if(i + 1 == rDefault[0])
                        {
                            model.Image = Image[i];
                            model.ProductImage.Add(new ProductImage
                            {
                                ProductId = model.Id,
                                Image = Image[i],
                                IsDefault = true
                            });
                        }
                        else
                        {
                            model.ProductImage.Add(new ProductImage
                            {
                                ProductId = model.Id,
                                Image = Image[i],
                                IsDefault = false
                            });
                        }
                    }
                }
                model.CreatedDate = DateTime.Now;
                model.ModifierDate = DateTime.Now;
                model.Alias = model.Alias == null ? Filter.FilterChar(model.Title) : model.Alias;
                model.SeoTitle = model.SeoTitle == null ? model.Title : model.SeoTitle;
                db.Products.Add(model);
                db.SaveChanges();
                return RedirectToAction("index");
            }
            ViewBag.ProductCategory = db.ProductCategories.ToList();
            return View(model); 
        }

        public ActionResult Edit(int id)
        {
            Product product = db.Products.Where(temp => temp.Id == id).FirstOrDefault();
            ViewBag.ProductCategory = db.ProductCategories.ToList();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, List<string> Image, List<int> rDefault)
        {
            if (ModelState.IsValid)
            {
                db.Products.Attach(product);
                if (Image != null && Image.Count > 0)
                {
                    var ImgExt = db.ProductImages.Where(temp => temp.ProductId == product.Id).ToList();

                    var ImgNew = Image.Where(x => !ImgExt.Any(z => z.Image == x)).ToList();

                    for (int i = 0; i < ImgNew.Count; i++)
                    {
                        product.ProductImage.Add(new ProductImage
                        {
                            ProductId = product.Id,
                            Image = ImgNew[i],
                            IsDefault = false
                        });
                    }

                    if(rDefault != null)
                    {
                        var rDefaults = rDefault[0];
                        var checkRdefault = db.ProductImages.Any(temp => temp.ProductId == product.Id && temp.Id == rDefaults);
                        if (checkRdefault)
                        {
                            var checkDefault = db.ProductImages.Any(temp => temp.ProductId == product.Id && temp.IsDefault == true);
                            if (checkDefault)
                            {
                                db.ProductImages.FirstOrDefault(temp => temp.ProductId == product.Id && temp.IsDefault == true).IsDefault = false;
                                db.ProductImages.FirstOrDefault(temp => temp.ProductId == product.Id && temp.Id == rDefaults).IsDefault = true;
                            }
                            db.ProductImages.FirstOrDefault(temp => temp.ProductId == product.Id && temp.Id == rDefaults).IsDefault = true;
                        }
                        //else if (rDefault.Count == 0 || !db.ProductImages.Any(temp => temp.ProductId == product.Id && temp.Id == rDefaults))
                        //{
                        //    ViewData["Loihinhdaidien"] = "Vui lòng lưu hình ảnh trước, sau đó chọn hãy làm ảnh đại diện";
                        //    ViewBag.ProductCategory = db.ProductCategories.ToList();
                        //    var prod = db.Products.Where(temp => temp.Id == product.Id).FirstOrDefault();
                        //    return View(prod);
                        //}
                    }

                    product.ModifierDate = DateTime.Now;
                    product.Alias = product.Alias == null ? Filter.FilterChar(product.Title) : product.Alias;
                    product.SeoTitle = product.SeoTitle == null ? product.Title : product.SeoTitle;
                    db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                
            }
            
            ViewBag.ProductCategory = db.ProductCategories.ToList();
            return View(product);
        }

        [HttpPost]
        public ActionResult IsHome(int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                item.IsHome = !item.IsHome;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, IsHome = item.IsHome });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult IsSale(int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                item.IsSale = !item.IsSale;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, IsSale = item.IsSale });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = db.Products.Find(id);
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
        public ActionResult IsFeature(int id)
        {
            var item = db.Products.Find(id);
            if (item != null)
            {
                item.IsFeature = !item.IsFeature;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, IsFeature = item.IsFeature });
            }

            return Json(new { success = false });
        }
    }
}


