using Data;
using Data.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Entity;

namespace AgriEnergyConnectPlatform.Controllers
{
    public class AdminController : Controller
    {
        private AgriConnectContext db = new AgriConnectContext();
        private UserManager<User> userManager;

        public AdminController()
        {
            userManager = new UserManager<User>(new UserStore<User>(db));
        }

        [Authorize(Roles = "Admin, Employee")]
        public ActionResult EmployeeDashboard()
        {
            var farmerRole = db.Roles.SingleOrDefault(r => r.Name == "Farmer");
            if (farmerRole == null)
            {
                // Handle case where the "Farmer" role does not exist
                ModelState.AddModelError("", "Farmer role not found.");
                return View();
            }

            var farmers = userManager.Users.Where(u => u.Roles.Any(r => r.RoleId == farmerRole.Id)).ToList();
            var products = db.Products.Include(p => p.User).ToList();

            ViewBag.Farmers = farmers;
            return View(products);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddEmployee(string employeeName, string employeeEmail, string employeePassword)
        {
            var employee = new User
            {
                Email = employeeEmail,
                Name = employeeName
            };

            var result = await userManager.CreateAsync(employee, employeePassword);
            if (result.Succeeded)
            {
                var employeeRole = db.Roles.SingleOrDefault(r => r.Name == "Employee");
                if (employeeRole != null)
                {
                    await userManager.AddToRoleAsync(employee.Id, "Employee");
                }
                else
                {
                    // Handle case where the "Employee" role does not exist
                    ModelState.AddModelError("", "Employee role not found.");
                    return RedirectToAction("EmployeeDashboard");
                }

                return RedirectToAction("EmployeeDashboard");
            }


            return RedirectToAction("EmployeeDashboard");
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Employee")]
        public async Task<ActionResult> AddFarmer(string farmerName, string farmerEmail, string farmerPassword)
        {
            var farmer = new User
            {
                UserName = farmerEmail,
                Email = farmerEmail,
                Name = farmerName
            };

            var result = await userManager.CreateAsync(farmer, farmerPassword);
            if (result.Succeeded)
            {
                var farmerRole = db.Roles.SingleOrDefault(r => r.Name == "Farmer");
                if (farmerRole != null)
                {
                    await userManager.AddToRoleAsync(farmer.Id, "Farmer");
                }
                else
                {
                    // Handle case where the "Farmer" role does not exist
                    ModelState.AddModelError("", "Farmer role not found.");
                    return RedirectToAction("EmployeeDashboard");
                }

                return RedirectToAction("EmployeeDashboard");
            }

            // Handle errors
            ModelState.AddModelError("", "Error adding farmer");
            return RedirectToAction("EmployeeDashboard");
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Employee")]
        public ActionResult FilterProducts(string farmerId, string productName, string category, DateTime? startDate, DateTime? endDate)
        {
            var products = db.Products.Include(p => p.User).AsQueryable();

            if (!string.IsNullOrEmpty(farmerId))
            {
                products = products.Where(p => p.UserId == farmerId);
            }

            if (!string.IsNullOrEmpty(productName))
            {
                products = products.Where(p => p.ProductName.Contains(productName));
            }

            if (!string.IsNullOrEmpty(category))
            {
                products = products.Where(p => p.Category.Contains(category));
            }

            if (startDate.HasValue)
            {
                products = products.Where(p => p.ProductionDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                products = products.Where(p => p.ProductionDate <= endDate.Value);
            }

            var farmerRole = db.Roles.SingleOrDefault(r => r.Name == "Farmer");
            if (farmerRole == null)
            {
                // Handle case where the "Farmer" role does not exist
                ModelState.AddModelError("", "Farmer role not found.");
                return View("EmployeeDashboard", products.ToList());
            }

            var farmers = userManager.Users.Where(u => u.Roles.Any(r => r.RoleId == farmerRole.Id)).ToList();

            ViewBag.Farmers = farmers;
            return View("EmployeeDashboard", products.ToList());
        }
    }
}
