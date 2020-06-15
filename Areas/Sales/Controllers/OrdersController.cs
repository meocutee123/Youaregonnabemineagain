using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Electronic_Store.Models;

namespace Electronic_Store.Areas.Sales.Controllers
{
    public class OrdersController : Controller
    {
        private ESDatabaseEntities db = new ESDatabaseEntities();

        // GET: Sales/Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Store).Include(o => o.Customer).Include(o => o.Staff);
            return View(orders.ToList());
        }

        //public ActionResult Details()
        // {
        //     return View();
        // }
        //GET: Sales/Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Sales/Orders/Create
        public ActionResult Create()
        {
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "StoreName");
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName");
            ViewBag.StaffID = new SelectList(db.Staffs, "StaffID", "FirstName");
            return View();
        }

        // POST: Sales/Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,CustomerID,StaffID,OrderDate,ShippedDate,StoreID,OrderStatus,Total")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "StoreName", order.StoreID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", order.CustomerID);
            ViewBag.StaffID = new SelectList(db.Staffs, "StaffID", "FirstName", order.StaffID);
            return View(order);
        }

        // GET: Sales/Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "StoreName", order.StoreID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", order.CustomerID);
            ViewBag.StaffID = new SelectList(db.Staffs, "StaffID", "FirstName", order.StaffID);
            return View(order);
        }

        // POST: Sales/Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,CustomerID,StaffID,OrderDate,ShippedDate,StoreID,OrderStatus,Total")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "StoreName", order.StoreID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", order.CustomerID);
            ViewBag.StaffID = new SelectList(db.Staffs, "StaffID", "FirstName", order.StaffID);
            return View(order);
        }

        // GET: Sales/Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Sales/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
