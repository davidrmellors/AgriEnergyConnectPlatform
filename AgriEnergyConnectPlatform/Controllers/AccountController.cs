using AgriEnergyConnectPlatform.ViewModels;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Data;
using Data.Extensions;

namespace AgriEnergyConnectPlatform.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<User, string> _signInManager;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private AgriConnectContext db = new AgriConnectContext();

        public AccountController()
        {
        }

        public AccountController(UserManager<User> userManager, SignInManager<User, string> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public UserManager<User> UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<UserManager<User>>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public SignInManager<User, string> SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<SignInManager<User, string>>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public RoleManager<IdentityRole> RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<RoleManager<IdentityRole>>();
            }
            private set
            {
                _roleManager = value;
            }
        }


        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Title = "Login";
            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult MyProfile()
        {
            string userId = User.Identity.GetUserId();
            var products = db.Products.Where(p => p.UserId == userId).ToList();
            return View(products);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
                if (result == SignInStatus.Success)
                {
                    if(UserManager.IsInRole(UserManager.FindByEmail(model.Email).Id, "Admin") ||
                        UserManager.IsInRole(UserManager.FindByEmail(model.Email).Id, "Employee"))
                    {
                        return RedirectToAction("EmployeeDashboard", "Admin");
                    }

                    return RedirectToAction("FarmingHub", "FarmingHub");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateProfile(string name, string email, string currentPassword, string newPassword, string confirmPassword)
        {


            // Update Profile Information
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                user.Name = name;
                user.Email = email;
                if (!string.IsNullOrEmpty(currentPassword) && !string.IsNullOrEmpty(newPassword) && !string.IsNullOrEmpty(confirmPassword))
                {
                    var passwordChangeResult = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), currentPassword, newPassword);
                    if (!passwordChangeResult.Succeeded)
                    {
                        AddErrors(passwordChangeResult);
                    }
                }
                var updateResult = await UserManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    AddErrors(updateResult);
                }
            }

            // Change Password if provided
            

            // Sign in the user with the updated information
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }

            return RedirectToAction("MyProfile", new { Message = ManageMessageId.ProfileUpdated });
        }



        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Email, Email = model.Email, Name = model.Name };
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, "Admin");
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    return RedirectToAction("Login", "Account");
                }
                AddErrors(result);
            }

            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            ProfileUpdated, // Add this line
            Error
        }
    }
}
