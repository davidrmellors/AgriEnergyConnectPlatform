using Data.Models;
using Data;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Antlr.Runtime;
using System.Threading.Tasks;

namespace AgriEnergyConnectPlatform
{
//-------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// The main application class for the AgriEnergyConnectPlatform.
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// The method that gets called when the application starts.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            string relativePath = @"..\Data\Database"; // Adjust the path as necessary
            string absolutePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath));
            Debug.WriteLine(absolutePath);
            AppDomain.CurrentDomain.SetData("DataDirectory", absolutePath);

            CreateRolesAndUsers();
        }

//-------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Creates the roles and users for the application.
        /// </summary>
        private void CreateRolesAndUsers()
        {
            AgriConnectContext context = new AgriConnectContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<User>(new UserStore<User>(context));

        //-------------------------------------------------------------------------------------------------------------
            // Creating Admin role if it does not exist
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

        //-------------------------------------------------------------------------------------------------------------
            // Creating default Admin user if it does not exist
            if (userManager.FindByEmail("admin@agriconnect.co.za") == null)
            {
                var admin = new User
                {
                    UserName = "admin@agriconnect.co.za",
                    Email = "admin@agriconnect.co.za",
                    Name = "Admin"
                };

                string adminPassword = "Admin@123";
                var result = userManager.Create(admin, adminPassword);
                if (result.Succeeded)
                {
                    userManager.AddToRole(admin.Id, "Admin");
                }
            }

        //-------------------------------------------------------------------------------------------------------------
            // Creating employee role if it does not exist
            if (!roleManager.RoleExists("Employee"))
            {
                var role = new IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);
            }

        //-------------------------------------------------------------------------------------------------------------
            if (userManager.FindByEmail("employee@agriconnect.co.za") == null)
            {
                var employee = new User
                {
                    UserName = "employee@agriconnect.co.za",
                    Email = "employee@agriconnect.co.za",
                    Name = "Employee"
                };

                string employeePassword = "Employee@123";
                var result = userManager.Create(employee, employeePassword);
                if (result.Succeeded)
                {
                    userManager.AddToRole(employee.Id, "Employee");
                }
            }

        //-------------------------------------------------------------------------------------------------------------
            // Creating Farmer role if it does not exist
            if (!roleManager.RoleExists("Farmer"))
            {
                var role = new IdentityRole();
                role.Name = "Farmer";
                roleManager.Create(role);
            }

        //-------------------------------------------------------------------------------------------------------------
            if (userManager.FindByEmail("farmer@agriconnect.co.za") == null)
            {
                var farmer = new User
                {
                    UserName = "farmer@agriconnect.co.za",
                    Email = "farmer@agriconnect.co.za",
                    Name = "Farmer"
                };

                string farmerPassword = "Farmer@123";
                var result = userManager.Create(farmer, farmerPassword);
                if (result.Succeeded)
                {
                    userManager.AddToRole(farmer.Id, "Farmer");
                }
            }
        //-------------------------------------------------------------------------------------------------------------
        }
    }
}
//-----------------------------------------------------END-OF-FILE-----------------------------------------------------//