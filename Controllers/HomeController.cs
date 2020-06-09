using Electronic_Store.Models;
using System;
using System.Collections.Generic;
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
            return View();
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