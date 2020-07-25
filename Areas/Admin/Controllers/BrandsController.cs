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
    public class BrandsController : Controller
    {
        private readonly ESDatabaseEntities db = new ESDatabaseEntities();
        [Authorize(Roles = "Admin")]
        
        // GET: Admin/Brands
        public ActionResult Index()
        {
            return View(db.Brands.ToList());
        }
        [Authorize(Roles = "Admin")]
        // GET: Admin/Brands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }
        [Authorize(Roles = "Admin")]
        // GET: Admin/Brands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Brands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BrandID,Name,BrandImg")] Brand brand, HttpPostedFileBase BrandImg)
        {
            if (ModelState.IsValid)
            {
                string postedFileName = System.IO.Path.GetFileName(BrandImg.FileName);
                //Lưu hình đại diện về Server
                var path = Server.MapPath("/Assets/images/" + postedFileName);
                BrandImg.SaveAs(path);
                brand.BrandImg = "/Assets/images/" + postedFileName;
                brand.Status = true;
                db.Brands.Add(brand);
                db.SaveChanges();   
                return RedirectToAction("Index");
            }

            return View(brand);
        }
        [Authorize(Roles = "Admin")]
        // GET: Admin/Brands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: Admin/Brands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BrandID,Name,BrandImg")] Brand brand, HttpPostedFileBase BrandImg)
        {
            if (ModelState.IsValid)
            {
                string postedFileName = System.IO.Path.GetFileName(BrandImg.FileName);
                //Lưu hình đại diện về Server
                var path = Server.MapPath("/Assets/images/" + postedFileName);
                BrandImg.SaveAs(path);
                brand.BrandImg = "/Assets/images/" + postedFileName;

                db.Entry(brand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(brand);
        }
        [Authorize(Roles = "Admin")]
        // GET: Admin/Brands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: Admin/Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Brand brand = db.Brands.Find(id);
            db.Brands.Remove(brand);
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
    }
}
