using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Electronic_Store.Models;

namespace Electronic_Store.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ESDatabaseEntities db = new ESDatabaseEntities();
        [Authorize(Roles = "Admin, Moderator")]
        // GET: Admin/Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Brand).Include(p => p.Category);
            return View(products.ToList());
        }
        [Authorize(Roles = "Admin, Moderator")]
        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        [Authorize(Roles = "Admin, Moderator")]
        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.BrandID = new SelectList(db.Brands, "BrandID", "Name");
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            return View();
        }

        // POST: Admin/Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,Name,BrandID,CategoryID,Price,ProductImg")]
        Product product, HttpPostedFileBase ProductImg)
        {
            if (ModelState.IsValid)
            {

                var isExist = IsProductNameExist(product.Name);
                if (!isExist)
                {
                    string postedFileName = Path.GetFileName(ProductImg.FileName);
                    var path = Server.MapPath("/Assets/images/" + postedFileName);
                    ProductImg.SaveAs(path);
                    product.ProductImg = "/Assets/images/" + postedFileName;

                    db.Products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                else
                {
                    ModelState.AddModelError("", "Product already exists");
                }

            }

            ViewBag.BrandID = new SelectList(db.Brands, "BrandID", "Name", product.BrandID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", product.CategoryID);
            return View(product);
        }
        [Authorize(Roles = "Admin, Moderator")]
        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandID = new SelectList(db.Brands, "BrandID", "Name", product.BrandID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", product.CategoryID);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,Name,BrandID,CategoryID,Price,ProductImg")]
        Product product, HttpPostedFileBase ProductImg)
        {
            if (ModelState.IsValid)
            {
                string postedFileName = Path.GetFileName(ProductImg.FileName);
                var path = Server.MapPath("/Assets/images/" + postedFileName);
                ProductImg.SaveAs(path);
                product.ProductImg = "/Assets/images/" + postedFileName;


                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandID = new SelectList(db.Brands, "BrandID", "Name", product.BrandID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", product.CategoryID);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [NonAction]
        public bool IsProductNameExist(string Name)
        {
            using (ESDatabaseEntities dc = new ESDatabaseEntities())
            {
                var v = dc.Products.Where(a => a.Name == Name).FirstOrDefault();
                return v != null;
            }
        }
    }
}
