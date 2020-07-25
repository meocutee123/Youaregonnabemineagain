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
    public class OrderItemsController : Controller
    {
        private readonly ESDatabaseEntities db = new ESDatabaseEntities();

        // GET: Sales/OrderItems
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var orderItem = db.OrderItems.Where(c => c.OrderID == id);
            var order = db.Orders.Where(c => c.OrderID == id).FirstOrDefault();
            ViewBag.CustomerName = order.Customer.FullName;
            if (orderItem == null)
            {
                return HttpNotFound();
            }

            return View(orderItem.ToList());
        }

        

        // GET: Sales/OrderItems/Create
        public ActionResult Create()
        {
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "OrderID");
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name");
            return View();
        }

        // POST: Sales/OrderItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,ProductID,Quanlity,Price")] OrderItem orderItem)
        {

            if (ModelState.IsValid)
            {
                var product = db.Products.Where(d => d.ProductID == orderItem.ProductID).FirstOrDefault();
                var stock = db.Stocks.Where(d => d.ProductID == orderItem.ProductID).FirstOrDefault();
                var maMax = db.Orders.ToList().Select(n => n.OrderID).Max();
                orderItem.OrderID = maMax;
                if(orderItem.Quanlity > stock.Quantity)
                {
                    ViewBag.Message = "This amount of product is not availble, there are only " + stock.Quantity + " left!";
                    ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "OrderID", orderItem.OrderID);
                    ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", orderItem.ProductID);
                    return View();
                }
                orderItem.Price = product.Price;
                db.OrderItems.Add(orderItem);
                db.SaveChanges();
                ViewBag.Message = "Added successfully!";
                ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "OrderID", orderItem.OrderID);
                ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", orderItem.ProductID);
                return View();
            }
            return View(orderItem);
        }

        // GET: Sales/OrderItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderItem orderItem = db.OrderItems.Find(id);
            if (orderItem == null)
            {
                return HttpNotFound();
            }
            return View(orderItem);
        }

        // POST: Sales/OrderItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderItem orderItem = db.OrderItems.Find(id);
            db.OrderItems.Remove(orderItem);
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
        public bool IsProductExist(int ProductID)
        {
            using (ESDatabaseEntities dc = new ESDatabaseEntities())
            {
                var v = dc.OrderItems.Where(a => a.ProductID == ProductID).FirstOrDefault();
                return v != null;
            }
        }
    }
}
