using Electronic_Store.Models;
using PagedList;
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
        private readonly ESDatabaseEntities db = new ESDatabaseEntities();

        public ActionResult Index(int page = 1, int pageSize = 8)
        {
            List<Product> products = (from p in db.Products where (p.Status == true) select p).ToList();
            var listProduct = new ListProduct();
            ViewBag.NewProducts = listProduct.listNewProduct(4);
            PagedList<Product> model = new PagedList<Product>(products, page, pageSize);
            return View(model);
        }
        //public ActionResult Index()
        //{
        //    dynamic dynamic = new ExpandoObject();
        //    dynamic.listProduct = Products();
        //    dynamic.listBrand = Brands();

        //    return View(dynamic);
        //}

        //public List<Product> Products()
        // {
        //    List<Product> lProducts = db.Products.ToList();
        //    return lProducts;
        //}
        //public List<Brand> Brands()
        //{
        //    List<Brand> lBrands = db.Brands.ToList();
        //    return lBrands;
        //}

        public ActionResult About()
        {
            var data = (from p in db.Staffs
                        select p).Take(4);
            return View(data.ToList());
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