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
    public class StaffsController : Controller
    {
        private readonly ESDatabaseEntities db = new ESDatabaseEntities();
        [Authorize(Roles = "Admin")]
        // GET: Admin/Staffs
        public ActionResult Index()
        {
            var staffs = db.Staffs.Include(s => s.Store);
            return View(staffs.ToList());
        }
        [Authorize(Roles = "Admin")]
        // GET: Admin/Staffs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }
        [Authorize(Roles = "Admin")]
        // GET: Admin/Staffs/Create
        public ActionResult Create()
        {
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "StoreName");
            ViewBag.DivisionID = new SelectList(db.Divisions, "DivisionID", "Name");
            return View();
        }

        // POST: Admin/Staffs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StaffID,FirstName,LastName,Email,Phone,Address,Password," +
            " ConfirmPassword,CreatedDate,ManagerID," +
            "ProfileImg,StoreID,Gender,Salary")] Staff staff, HttpPostedFileBase ProfileImg)
        {
            if (ModelState.IsValid)
            {
                var isExist = IsEmailExist(staff.Email);
                if (!isExist)
                {
                    string postedFileName = System.IO.Path.GetFileName(ProfileImg.FileName);
                    //Lưu hình đại diện về Server
                    var path = Server.MapPath("~/Assets/images/Staffs/" + postedFileName);
                    ProfileImg.SaveAs(path);
                    staff.ProfileImg = "~/Assets/images/Staffs/" + postedFileName;
                    staff.CreatedDate = DateTime.Now;
                    db.Staffs.Add(staff);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                    
                } else
                {
                    ModelState.AddModelError("", "Email already exist");
                }
            }
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "StoreName", staff.StoreID);
            ViewBag.DivisionID = new SelectList(db.Divisions, "DivisionID", "Name", staff.DivisionID);
            return View(staff);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult EditRole(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "StoreName", staff.StoreID);
            ViewBag.DivisionID = new SelectList(db.Divisions, "DivisionID", "Name", staff.DivisionID);
            return View(staff);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRole([Bind(Include = "StaffID,FirstName,LastName,Email,Phone,Address,Password, ConfirmPassword,CreatedDate,ManagerID,ProfileImg,StoreID,Gender,Salary")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                var currentStaff = db.Staffs.FirstOrDefault(s => s.StaffID == staff.StaffID);
                if (currentStaff == null)
                {
                    return HttpNotFound();
                }
                currentStaff.StoreID = staff.StoreID;
                currentStaff.Role = staff.Role;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(staff);
        }
        [Authorize(Roles = "Admin")]
        // GET: Admin/Staffs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "StoreName", staff.StoreID);
            ViewBag.DivisionID = new SelectList(db.Divisions, "DivisionID", "Name", staff.DivisionID);
            return View(staff);
        }

        // POST: Admin/Staffs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StaffID,FirstName,LastName,Email,Phone,Address,Password, ConfirmPassword,CreatedDate,ManagerID,ProfileImg,StoreID,Gender,Salary")] Staff staff, HttpPostedFileBase ProfileImg)
        {
            if (ModelState.IsValid)
            {
                string postedFileName = System.IO.Path.GetFileName(ProfileImg.FileName);
                //Lưu hình đại diện về Server
                var path = Server.MapPath("~/Assets/images/Staffs/" + postedFileName);
                ProfileImg.SaveAs(path);
                staff.ProfileImg = "~/Assets/images/Staffs/" + postedFileName;

                db.Entry(staff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StoreID = new SelectList(db.Stores, "StoreID", "StoreName", staff.StoreID);
            ViewBag.DivisionID = new SelectList(db.Divisions, "DivisionID", "Name", staff.DivisionID);
            return View(staff);
        }
        [Authorize(Roles = "Admin")]
        // GET: Admin/Staffs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // POST: Admin/Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Staff staff = db.Staffs.Find(id);
            db.Staffs.Remove(staff);
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
