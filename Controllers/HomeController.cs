using Electronic_Store.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Electronic_Store.Controllers
{

    public class HomeController : Controller
    {
        private readonly ESDatabaseEntities db = new ESDatabaseEntities();

        public ActionResult Index(int page = 1, int pageSize = 8)
        {
            List<Product> products = (from p in db.Products where (p.Status == true) select p).ToList();
            var listProduct = new ListProduct();
            ViewBag.NewProducts = listProduct.listNewProduct(4);
            ViewBag.ListPhone = listProduct.listPhone();
            ViewBag.ListBrands = listProduct.listBrand();
            ViewBag.ListHouseWare = listProduct.listHouseWare();
            PagedList<Product> model = new PagedList<Product>(products, page, pageSize);
            return View(model);
        }
        public ActionResult ListPhone()
        {
            var listProduct = new ListProduct();
            ViewBag.ListPhone = listProduct.listPhone();
            return View();
        }
        public ActionResult ListHouseWare()
        {
            var listProduct = new ListProduct();
            ViewBag.ListHouseWare = listProduct.listHouseWare();
            return View();
        }

        public ActionResult About()
        {
            var data = (from p in db.Staffs
                        where p.Status == true
                        select p).Take(8);
            return View(data.ToList());
        }
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
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
                contact.DateSent = DateTime.Now;
                db.Conntacts.Add(contact);
                db.SaveChanges();
            }

            return View();
        }

        public ActionResult Search(string search, int page = 1, int pageSize = 8)
        {

            List<Product> products = (from p in db.Products where p.Name.Contains(search) select p).ToList();
            if (products.Count() == 0)
            {
                ViewBag.Message = "Nothing was found!";
            }
            PagedList<Product> model = new PagedList<Product>(products, page, pageSize);
            return View(model);


        }
    }
}