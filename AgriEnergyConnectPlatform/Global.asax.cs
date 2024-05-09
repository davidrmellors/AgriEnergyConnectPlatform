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

        }
    }
}
