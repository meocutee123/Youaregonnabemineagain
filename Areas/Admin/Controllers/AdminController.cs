using Electronic_Store.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Electronic_Store.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        readonly ESDatabaseEntities db = new ESDatabaseEntities();
        [Authorize]
        public ActionResult Index() 
        {

            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult TimKiemNC(string FullName = "", string Gender = "", string luongMin = "", string luongMax = "", string Address = "")
        {
            string max = luongMax;
            if (Gender != "1" && Gender != "0")
                Gender = null;

            ViewBag.hoTen = FullName;
            ViewBag.gioiTinh = Gender;
            string min;
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

        [HttpGet]
        [Authorize]
        public ActionResult TimKiemMH(string Name = "", string Brand = "", string Category = "", string PriceMin = "", string PriceMax = "")
        {
            string max = PriceMax;

            ViewBag.Name = Name;
            ViewBag.Brand = Brand;
            string min;
            if (PriceMin == "")
            {
                ViewBag.PriceMin = "";
                min = "0";
            }
            else
            {
                ViewBag.PriceMin = PriceMin;
                min = PriceMin;
            }
            if (max == "")
            {
                max = Int32.MaxValue.ToString();
                ViewBag.PriceMax = "";// Int32.MaxValue.ToString(); 
            }
            else
            {
                ViewBag.PriceMax = PriceMax;
                max = PriceMax;
            }


            var staffs = db.Products.SqlQuery("MatHang_TimKiem'" + Name + "','" + Brand + "','" + Category + "','" + min + "','" + max + "'");
            if (staffs.Count() == 0)
                ViewBag.TB = "Empty";
            return View(staffs.ToList());
        }
    }
}