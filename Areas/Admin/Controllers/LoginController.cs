using Electronic_Store.Areas.Admin.Model;
using Electronic_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Electronic_Store.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        readonly ESDatabaseEntities db = new ESDatabaseEntities();
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Adminstrator adminstrator, string returnUrl)
        {
            var dataItem = db.Adminstrators.Where(x => x.AdminName == adminstrator.AdminName && x.Password == adminstrator.Password).First();
            if (dataItem != null)
            {
                FormsAuthentication.SetAuthCookie(dataItem.AdminName, false);
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                         && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index","Admin");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid adminname and password");
                return View();
            }
            
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login","Login");
        }
    }
}