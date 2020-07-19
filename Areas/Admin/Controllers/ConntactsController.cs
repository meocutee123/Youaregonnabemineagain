using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Electronic_Store.Models;

namespace Electronic_Store.Areas.Admin.Controllers
{
    public class ConntactsController : Controller
    {
        private ESDatabaseEntities db = new ESDatabaseEntities();

        // GET: Admin/Conntacts
        public ActionResult Index()
        {
            return View(db.Conntacts.ToList());
        }

        // GET: Admin/Conntacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conntact conntact = db.Conntacts.Find(id);
            if (conntact == null)
            {
                return HttpNotFound();
            }
            return View(conntact);
        }

        // GET: Admin/Conntacts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Conntacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Message,Email,DateSent,Subject")] Conntact conntact)
        {
            if (ModelState.IsValid)
            {
                db.Conntacts.Add(conntact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(conntact);
        }

        // GET: Admin/Conntacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conntact conntact = db.Conntacts.Find(id);
            if (conntact == null)
            {
                return HttpNotFound();
            }
            return View(conntact);
        }

        // POST: Admin/Conntacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Message,Email,DateSent,Subject")] Conntact conntact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(conntact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(conntact);
        }

        // GET: Admin/Conntacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conntact conntact = db.Conntacts.Find(id);
            if (conntact == null)
            {
                return HttpNotFound();
            }
            return View(conntact);
        }

        // POST: Admin/Conntacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Conntact conntact = db.Conntacts.Find(id);
            db.Conntacts.Remove(conntact);
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
