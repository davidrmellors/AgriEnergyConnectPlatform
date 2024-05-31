using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AgriEnergyConnectPlatform
{
    /// <summary>
    /// Configures the routes for the application.
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Registers the routes for the application.
        /// </summary>
        /// <param name="routes">The collection of routes for the application.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
//-----------------------------------------------------END-OF-FILE-----------------------------------------------------//