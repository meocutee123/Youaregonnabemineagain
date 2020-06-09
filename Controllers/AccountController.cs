﻿using Electronic_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Electronic_Store.Controllers
{
    public class AccountController : Controller
    {

        public ActionResult Resgistration()
        {
            return View();
        }
        
        public ActionResult Registration(Customer customer)
        {
            using (ESDatabaseEntities db = new ESDatabaseEntities())
            {
                if (ModelState.IsValid)
                {
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                return View(customer);
            }
        }
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Customer customer)
        {
            using (var context = new ESDatabaseEntities())
            {
                bool isValid = context.Customers.Any(x => x.Email == customer.Email && x.Password == customer.Password);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(customer.Email, false);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid username and password");
                return View();
            }
            
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}