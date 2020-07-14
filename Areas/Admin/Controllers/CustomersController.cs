using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Electronic_Store.Models;

namespace Electronic_Store.Areas.Admin.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ESDatabaseEntities db = new ESDatabaseEntities();
        [Authorize(Roles = "Admin, Moderator")]
        // GET: Admin/Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        //[HttpPost]
        //public JsonResult CheckMail(string email)
        //{
        //    bool result = !db.Customers.ToList().Exists(model => model.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase));
        //    return Json(result);
        //}
        [Authorize(Roles = "Admin, Moderator")]
        // GET: Admin/Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        [Authorize(Roles = "Admin, Moderator")]
        // GET: Admin/Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,FirstName,LastName,Email" +
            ",Address,Password, ConfirmPassword, CreatedDate, ProfileImg, Status")] Customer customer)
        {

            if (ModelState.IsValid)
            {
                var isExist = IsEmailExist(customer.Email);
                if (isExist)
                {
                    ModelState.AddModelError("", "Email already exists");
                    return View(customer);
                }
                customer.Password = Crypto.Hash(customer.Password);
                customer.ConfirmPassword = Crypto.Hash(customer.ConfirmPassword);
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }
        [Authorize(Roles = "Admin, Moderator")]
        // GET: Admin/Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Admin/Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,FirstName,LastName,Email,Address,Password, ConfirmPassword, CreatedDate, ProfileImg, Status")] Customer customer, HttpPostedFileBase ProfileImg)
        {
            if (ModelState.IsValid)
            {
                string postedFileName = System.IO.Path.GetFileName(ProfileImg.FileName);
                //Lưu hình đại diện về Server
                var path = Server.MapPath("~/Assets/images/Customers/" + postedFileName);
                ProfileImg.SaveAs(path);
                customer.ProfileImg = "~/Assets/images/Customers/" + postedFileName;

                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }
        [Authorize(Roles = "Admin, Moderator")]
        // GET: Admin/Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Admin/Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            var currentCustomer = db.Customers.FirstOrDefault(s => s.CustomerID == id);
            if (currentCustomer == null)
            {
                return HttpNotFound();
            }
            currentCustomer.Status = false;
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
        public bool IsEmailExist(string Email)
        {
            using (ESDatabaseEntities dc = new ESDatabaseEntities())
            {
                var v = dc.Customers.Where(a => a.Email == Email).FirstOrDefault();
                return v != null;
            }
        }
    }
}
