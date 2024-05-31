using Data;
using Data.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Entity;
using System.Diagnostics;
using System.Web.Helpers;

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
        public ActionResult Index()
        {
            var farmerRole = db.Roles.SingleOrDefault(r => r.Name == "Farmer");
            var employeeRole = db.Roles.SingleOrDefault(r => r.Name == "Employee");
            if (farmerRole == null)
            {
                // Handle case where the "Farmer" role does not exist
                ModelState.AddModelError("", "Farmer role not found.");
                return View();
            }

            var farmers = userManager.Users.Where(u => u.Roles.Any(r => r.RoleId == farmerRole.Id)).ToList();
            var employees = userManager.Users.Where(u => u.Roles.Any(r => r.RoleId == employeeRole.Id)).ToList();
            
            var products = db.Products.Include(p => p.User).ToList();

            ViewBag.Farmers = farmers;
            ViewBag.Employees = employees;
            return View(products);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddEmployee(string employeeName, string employeeEmail, string employeePassword)
        {

            var employee = new User
            {
                UserName = employeeEmail,
                Name = employeeName,
                Email = employeeEmail
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
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }

            // Handle errors
            ModelState.AddModelError("", "Error adding employee");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteEmployee(string employeeId)
        {
            var employee = await userManager.FindByIdAsync(employeeId);
            if (employee == null)
            {
                ModelState.AddModelError("", "Employee not found.");
                return RedirectToAction("Index");
            }

            var result = await userManager.DeleteAsync(employee);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Error deleting employee");
            return RedirectToAction("Index");
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
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }

            // Handle errors
            ModelState.AddModelError("", "Error adding farmer");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteFarmer(string farmerId)
        {
            var farmer = await userManager.FindByIdAsync(farmerId);
            if (farmer == null)
            {
                ModelState.AddModelError("", "Farmer not found.");
                return RedirectToAction("Index");
            }

            // Delete all products associated with the farmer
            var products = db.Products.Where(p => p.UserId == farmerId);
            db.Products.RemoveRange(products);

            var result = await userManager.DeleteAsync(farmer);
            if (result.Succeeded)
            {
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Error deleting farmer");
            return RedirectToAction("Index");
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
                products = products.Where(p => p.ProductionDate >= startDate);
            }

            if (endDate.HasValue)
            {
                products = products.Where(p => p.ProductionDate <= endDate);
            }

            var farmerRole = db.Roles.SingleOrDefault(r => r.Name == "Farmer");
            if (farmerRole == null)
            {
                // Handle case where the "Farmer" role does not exist
                ModelState.AddModelError("", "Farmer role not found.");
                return View("Index");
            }

            var employeeRole = db.Roles.SingleOrDefault(r => r.Name == "Employee");
            if (farmerRole == null)
            {
                // Handle case where the "Farmer" role does not exist
                ModelState.AddModelError("", "Employee role not found.");
                return View("Index");
            }

            var farmers = userManager.Users.Where(u => u.Roles.Any(r => r.RoleId == farmerRole.Id)).ToList();
            var employees = userManager.Users.Where(u => u.Roles.Any(r => r.RoleId == employeeRole.Id)).ToList();

            ViewBag.Farmers = farmers;
            ViewBag.Employees = employees;
            ViewBag.FarmerId = farmerId;
            ViewBag.ProductName = productName;
            ViewBag.Category = category;
            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");

            return View("Index", products.ToList());
        }
    }
}
