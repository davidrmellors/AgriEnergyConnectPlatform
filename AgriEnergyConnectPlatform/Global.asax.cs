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
    public class MvcApplication : System.Web.HttpApplication
    {
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

        private void CreateRolesAndUsers()
        {
            AgriConnectContext context = new AgriConnectContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<User>(new UserStore<User>(context));

            // In Startup creating first Admin Role and creating a default Admin User 
            if (roleManager.RoleExists("Admin"))
            {

                var admin = new User
                {
                    UserName = "david",
                    Email = "david@admin.com",
                    Name = "david admin"
                };

                string adminPassword = "Admin@123";

                var result = userManager.Create(admin, adminPassword);
                if (result.Succeeded)
                {
                    var adminRole = context.Roles.SingleOrDefault(r => r.Name == "Admin");
                    if (adminRole != null)
                    {
                        userManager.AddToRoleAsync(admin.Id, "Admin");
                    }
                }
               
            }

            // Creating Farmer role    
            if (!roleManager.RoleExists("Farmer"))
            {
                var role = new IdentityRole();
                role.Name = "Farmer";
                roleManager.Create(role);
            }
        }
    }
}
