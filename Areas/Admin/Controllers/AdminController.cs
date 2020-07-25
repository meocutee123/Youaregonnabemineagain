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
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult Index() 
        {

            return View();
        }

        
    }
}