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
        private readonly ESDatabaseEntities db = new ESDatabaseEntities();

        // GET: Sales/Orders
        [Authorize(Roles ="Admin, Moderator")]

        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Store).Include(o => o.Customer).Include(o => o.Staff).Include(d => d.OrderItems).ToList();
            foreach(var order in orders)
            {
                decimal Total = 0;
                foreach(var orderItem in order.OrderItems)
                {
                    decimal quantityDecimal = orderItem.Quanlity ?? 0;
                    decimal priceDecimal = orderItem.Price ?? 0;
                    Total += quantityDecimal * priceDecimal;
                   
                }        
                order.Total = Total;

            }
            Session["Message"] = "Mèo méo meo mèo meo";
            return View(orders);
        }


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
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult Create()
        {
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "StoreName");
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FullName");
            ViewBag.StaffID = new SelectList(db.Staffs, "StaffID", "FullName");
            return View();
        }

        // POST: Sales/Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,CustomerID,StaffID," +
            "OrderDate,ShippedDate,StoreID,OrderStatus,Total,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.OrderDate = DateTime.Now;
                db.Orders.Add(order);
                db.SaveChanges();
                
            }
            var id = db.Orders.ToList().Select(n => n.OrderID).Max();
            return RedirectToAction("Index", "OrderItems", new { id });
            //ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "StoreName", order.StoreID);
            //ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", order.CustomerID);
            //ViewBag.StaffID = new SelectList(db.Staffs, "StaffID", "FirstName", order.StaffID);
            //return View(order);
        }
        [Authorize(Roles = "Admin, Moderator")]
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
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FullName", order.CustomerID);
            ViewBag.StaffID = new SelectList(db.Staffs, "StaffID", "FullName", order.StaffID);
            return View(order);
        }

        // POST: Sales/Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,CustomerID,StaffID,OrderDate,ShippedDate,StoreID,OrderStatus,Total,Status")] Order order)
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
        [Authorize(Roles = "Admin, Moderator")]
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
            order.Status = false;
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
