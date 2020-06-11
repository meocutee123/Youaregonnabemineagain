using Electronic_Store.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Electronic_Store.Controllers
{
    
    public class HomeController : Controller
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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Contact(Conntact contact)
        {
            if (ModelState.IsValid)
            {
                db.Conntacts.Add(contact);
                db.SaveChanges();
            }

            return View();
        }
    }
}