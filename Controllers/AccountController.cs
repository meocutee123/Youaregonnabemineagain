using Electronic_Store.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Electronic_Store.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(Customer customer)
        {
            // Model Validation 
            if (ModelState.IsValid)
            {

                var isExist = IsEmailExist(customer.Email);
                if (isExist)
                {
                    ModelState.AddModelError("", "Email already exist");
                    return View(customer);
                }


                customer.Password = Crypto.Hash(customer.Password);
                customer.ConfirmPassword = Crypto.Hash(customer.ConfirmPassword);

                using (ESDatabaseEntities dc = new ESDatabaseEntities())
                {
                    dc.Customers.Add(customer);
                    dc.SaveChanges();
                }

                ViewBag.Message = "Account created successfully, please log-in!";
            }

            return View("Login");
        }
        [HttpGet]
        [AllowAnonymous]
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Customer customer)
        {
            using (var db = new ESDatabaseEntities())
            {
                var v = db.Customers.Where(a => a.Email == customer.Email).FirstOrDefault();
                if (v != null)
                {
                    if (string.Compare(Crypto.Hash(customer.Password), v.Password) == 0)
                    {
                        FormsAuthentication.SetAuthCookie(customer.Email, false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid username and password");
                        return View("Login");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username and password");
                    return View("Login");
                }
            }
            
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [NonAction]
        public bool IsEmailExist(string Email)
        {
            using (ESDatabaseEntities dc = new ESDatabaseEntities())
            {
                var v = dc.Customers.Where(a => a.Email == Email).FirstOrDefault();
                return v != null;
            }
        }










        public ActionResult Index2()
        {
            dynamic dy = new ExpandoObject();
            dy.listCustomer = GetCustomers();
            dy.listProduct = GetProducts();
            return View(dy);
        }

        public List<Customer> GetCustomers()
        {
            ESDatabaseEntities eS = new ESDatabaseEntities();
            List<Customer> lCustomer = eS.Customers.ToList();
            return lCustomer;

        }
        public List<Product> GetProducts()
        {
            ESDatabaseEntities eS = new ESDatabaseEntities();
            List<Product> lProduct = eS.Products.ToList();
            return lProduct;

        }



    }
}