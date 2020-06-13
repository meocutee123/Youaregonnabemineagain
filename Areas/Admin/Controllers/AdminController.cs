﻿using Electronic_Store.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Electronic_Store.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        readonly ESDatabaseEntities db = new ESDatabaseEntities();

        public ActionResult Index() 
        {
            dynamic dynamic = new ExpandoObject();
            dynamic.listProduct = Products();
            dynamic.listBrand = Brands();

            return View(dynamic);
        }
        public List<Product> Products()
        {
            List<Product> lProducts = db.Products.ToList();
            return lProducts;
        }
        public List<Brand> Brands()
        {
            List<Brand> lBrands = db.Brands.ToList();
            return lBrands;
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

        [HttpGet]

        public ActionResult TimKiemMH(string Name = "", string Brand = "", string Category = "", string PriceMin = "", string PriceMax = "")
        {
            string min = PriceMin, max = PriceMax;

            ViewBag.Name = Name;
            ViewBag.Brand = Brand;

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