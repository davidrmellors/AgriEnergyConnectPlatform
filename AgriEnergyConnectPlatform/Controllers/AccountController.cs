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
//-------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Controller for managing user accounts.
    /// </summary>
    public class AccountController : Controller
    {
        private SignInManager<User, string> _signInManager;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private AgriConnectContext db = new AgriConnectContext();

//-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Default constructor.
        /// </summary>
        public AccountController()
        {
        }

//-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        public AccountController(UserManager<User> userManager, SignInManager<User, string> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

//-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// User manager property.
        /// </summary>
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

//-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Sign in manager property.
        /// </summary>
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

//-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Role manager property.
        /// </summary>
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

//-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// GET: Account
        /// </summary>
        public ActionResult Index()
        {
            return View();
        }

//-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// GET: Login
        /// </summary>
        public ActionResult Login()
        {
            ViewBag.Title = "Login";
            return View();
        }

//-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// POST: Logout
        /// </summary>
        [Authorize]
        public ActionResult Logout()
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

//-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// GET: MyProfile
        /// </summary>
        [Authorize]
        public ActionResult MyProfile()
        {
            string userId = User.Identity.GetUserId();
            var products = db.Products.Where(p => p.UserId == userId).ToList();
            return View(products);
        }

//-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// POST: Login
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }

            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, shouldLockout: false);
            if (result != SignInStatus.Success)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }

            var user = await UserManager.FindByEmailAsync(model.Email);
            if (UserManager.IsInRole(user.Id, "Admin") || UserManager.IsInRole(user.Id, "Employee"))
            {
                return RedirectToAction("Index", "Admin");
            }

            return RedirectToAction("Index", "FarmingHub");
        }

//-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// POST: UpdateProfile
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateProfile(string name, string email, string currentPassword, string newPassword, string confirmPassword)
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return RedirectToAction("MyProfile", new { Message = ManageMessageId.Error });
            }

            user.Name = name;
            user.Email = email;

            if (!string.IsNullOrEmpty(currentPassword) && !string.IsNullOrEmpty(newPassword) && !string.IsNullOrEmpty(confirmPassword))
            {
                var passwordChangeResult = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), currentPassword, newPassword);
                if (!passwordChangeResult.Succeeded)
                {
                    AddErrors(passwordChangeResult);
                    return RedirectToAction("MyProfile", new { Message = ManageMessageId.Error });
                }
            }

            var updateResult = await UserManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                AddErrors(updateResult);
                return RedirectToAction("MyProfile", new { Message = ManageMessageId.Error });
            }

            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

            return RedirectToAction("MyProfile", new { Message = ManageMessageId.ProfileUpdated });
        }

//-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// GET: Register
        /// </summary>
        public ActionResult Register()
        {
            return View();
        }

//-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// POST: Register
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User { UserName = model.Email, Email = model.Email, Name = model.Name };
            var result = await UserManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                AddErrors(result);
                return View(model);
            }

            await UserManager.AddToRoleAsync(user.Id, "Admin");
            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            return RedirectToAction("Login", "Account");
        }

//-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Adds errors to the ModelState.
        /// </summary>
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

//-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Enum for managing messages.
        /// </summary>
        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            ProfileUpdated,
            Error
        }
    }
}
//-----------------------------------------------------END-OF-FILE-----------------------------------------------------//