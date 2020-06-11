using Electronic_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Electronic_Store.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private readonly ESDatabaseEntities db = new ESDatabaseEntities();
        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]

        public ActionResult TimKiemNC(string FullName = "", string Gender = "", string luongMin = "", string luongMax = "", string Address = "")
        {
            string min = luongMin, max = luongMax;
            if (Gender != "1" && Gender != "0")
                Gender = null;
           
            ViewBag.hoTen = FullName;
            ViewBag.gioiTinh = Gender;
            if (luongMin == "")
            {
                ViewBag.luongMin = "";
                min = "0";
            }
            else
            {
                ViewBag.luongMin = luongMin;
                min = luongMin;
            }
            if (max == "")
            {
                max = Int32.MaxValue.ToString();
                ViewBag.luongMax = "";// Int32.MaxValue.ToString(); 
            }
            else
            {
                ViewBag.luongMax = luongMax;
                max = luongMax;
            }
            ViewBag.diaChi = Address;
            
            var staffs = db.Staffs.SqlQuery("NhanVien_TimKiem'" + FullName + "','" + Gender + "','" + min + "','" + max + "',N'" + Address + "'");
            if (staffs.Count() == 0)
                ViewBag.TB = "Empty";
            return View(staffs.ToList());
        }
    }
}