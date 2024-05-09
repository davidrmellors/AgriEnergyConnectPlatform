using AgriEnergyConnectPlatform.ViewModels;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AgriEnergyConnectPlatform.Controllers
{
    public class AccountController : Controller
    {

        private Data.AgriConnectContext db = new Data.AgriConnectContext();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = db.Users.FirstOrDefault(u => 
                u.Email == model.Email && u.Password == model.Password);

                if(user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                }
            }

            return View(model);
        }

        public ActionResult Register() 
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var exisitingUser = db.Users.FirstOrDefault(u =>
                u.Email == model.Email);

                if(exisitingUser == null)
                {
                    var user = new User
                    {
                        Name = model.Name,
                        Email = model.Email,
                        Password = model.Password, // Hash this password before saving
                                                   // Set other default values, if any
                    };

                    db.Users.Add(user);
                    db.SaveChanges();

                    // Optionally log in the user and redirect them
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Email address already in use.");
                }
                
            }

            return View(model);
        }
    }
}