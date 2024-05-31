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
//-------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// AdminController is a class that inherits from the Controller class.
    /// It is responsible for handling administrative actions such as managing users and products.
    /// </summary>
    public class AdminController : Controller
    {
//-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// The database context used to interact with the database.
        /// </summary>
        private AgriConnectContext db = new AgriConnectContext();

//-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// The user manager used to manage users in the database.
        /// </summary>
        private UserManager<User> userManager;

//-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// The constructor for the AdminController class.
        /// Initializes a new instance of the UserManager class.
        /// </summary>
        public AdminController()
        {
            userManager = new UserManager<User>(new UserStore<User>(db));
        }

//-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Returns the Index view of the Admin section.
        /// This method is authorized for users with the "Admin" or "Employee" role.
        /// </summary>
        /// <returns>The Index view of the Admin section.</returns>

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

//-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Adds a new employee.
        /// This method is authorized for users with the "Admin" role.
        /// </summary>
        /// <param name="employeeName">The name of the employee.</param>
        /// <param name="employeeEmail">The email of the employee.</param>
        /// <param name="employeePassword">The password of the employee.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddEmployee(string employeeName, string employeeEmail, string employeePassword)
        {
        //-------------------------------------------------------------------------------------------------------------
            var employee = new User
            {
                UserName = employeeEmail,
                Name = employeeName,
                Email = employeeEmail
            };

        //-------------------------------------------------------------------------------------------------------------
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

        //-------------------------------------------------------------------------------------------------------------
            // Handle errors
            ModelState.AddModelError("", "Error adding employee");
            return RedirectToAction("Index");
        //-------------------------------------------------------------------------------------------------------------
        }

        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Deletes an existing employee.
        /// This method is authorized for users with the "Admin" role.
        /// </summary>
        /// <param name="employeeId">The ID of the employee.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteEmployee(string employeeId)
        {
        //-------------------------------------------------------------------------------------------------------------
            var employee = await userManager.FindByIdAsync(employeeId);
            if (employee == null)
            {
                ModelState.AddModelError("", "Employee not found.");
                return RedirectToAction("Index");
            }

        //-------------------------------------------------------------------------------------------------------------
            var result = await userManager.DeleteAsync(employee);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Error deleting employee");
            return RedirectToAction("Index");
        //-------------------------------------------------------------------------------------------------------------
        }

        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Adds a new farmer.
        /// This method is authorized for users with the "Admin" or "Employee" role.
        /// </summary>
        /// <param name="farmerName">The name of the farmer.</param>
        /// <param name="farmerEmail">The email of the farmer.</param>
        /// <param name="farmerPassword">The password of the farmer.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        [HttpPost]
        [Authorize(Roles = "Admin, Employee")]
        public async Task<ActionResult> AddFarmer(string farmerName, string farmerEmail, string farmerPassword)
        {

        //-------------------------------------------------------------------------------------------------------------
            var farmer = new User
            {
                UserName = farmerEmail,
                Email = farmerEmail,
                Name = farmerName
            };

        //-------------------------------------------------------------------------------------------------------------
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
         //-------------------------------------------------------------------------------------------------------------
            // Handle errors
            ModelState.AddModelError("", "Error adding farmer");
            return RedirectToAction("Index");
         //-------------------------------------------------------------------------------------------------------------
        }

        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Deletes an existing farmer.
        /// This method is authorized for users with the "Admin" role.
        /// </summary>
        /// <param name="farmerId">The ID of the farmer.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteFarmer(string farmerId)
        {
        //-------------------------------------------------------------------------------------------------------------
            var farmer = await userManager.FindByIdAsync(farmerId);
            if (farmer == null)
            {
                ModelState.AddModelError("", "Farmer not found.");
                return RedirectToAction("Index");
            }
        //-------------------------------------------------------------------------------------------------------------
            // Delete all products associated with the farmer
            var products = db.Products.Where(p => p.UserId == farmerId);
            db.Products.RemoveRange(products);
        
            var result = await userManager.DeleteAsync(farmer);
            if (result.Succeeded)
            {
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

        //-------------------------------------------------------------------------------------------------------------
            ModelState.AddModelError("", "Error deleting farmer");
            return RedirectToAction("Index");
        //-------------------------------------------------------------------------------------------------------------
        }
        //-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Filters products based on the provided parameters.
        /// This method is authorized for users with the "Admin" or "Employee" role.
        /// </summary>
        /// <param name="farmerId">The ID of the farmer.</param>
        /// <param name="productName">The name of the product.</param>
        /// <param name="category">The category of the product.</param>
        /// <param name="startDate">The start date of the production.</param>
        /// <param name="endDate">The end date of the production.</param>
        /// <returns>The Index view with the filtered products.</returns>
        [HttpGet]
        [Authorize(Roles = "Admin, Employee")]
        public ActionResult FilterProducts(string farmerId, string productName, string category, DateTime? startDate, DateTime? endDate)
        {
        //-------------------------------------------------------------------------------------------------------------
            var products = db.Products.Include(p => p.User).AsQueryable();
        
            if (!string.IsNullOrEmpty(farmerId))
            {
                products = products.Where(p => p.UserId == farmerId);
            }

        //-------------------------------------------------------------------------------------------------------------
            if (!string.IsNullOrEmpty(productName))
            {
                products = products.Where(p => p.ProductName.Contains(productName));
            }

        //-------------------------------------------------------------------------------------------------------------
            if (!string.IsNullOrEmpty(category))
            {
                products = products.Where(p => p.Category.Contains(category));
            }

        //-------------------------------------------------------------------------------------------------------------
            if (startDate.HasValue)
            {
                products = products.Where(p => p.ProductionDate >= startDate);
            }

        //-------------------------------------------------------------------------------------------------------------
            if (endDate.HasValue)
            {
                products = products.Where(p => p.ProductionDate <= endDate);
            }

        //-------------------------------------------------------------------------------------------------------------
            var farmerRole = db.Roles.SingleOrDefault(r => r.Name == "Farmer");
            if (farmerRole == null)
            {
                // Handle case where the "Farmer" role does not exist
                ModelState.AddModelError("", "Farmer role not found.");
                return View("Index");
            }

        //-------------------------------------------------------------------------------------------------------------
            var employeeRole = db.Roles.SingleOrDefault(r => r.Name == "Employee");
            if (farmerRole == null)
            {
                // Handle case where the "Farmer" role does not exist
                ModelState.AddModelError("", "Employee role not found.");
                return View("Index");
            }

        //-------------------------------------------------------------------------------------------------------------
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

        //-------------------------------------------------------------------------------------------------------------
        }
        //-------------------------------------------------------------------------------------------------------------
    }
}
//-----------------------------------------------------END-OF-FILE-----------------------------------------------------//