using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgriEnergyConnectPlatform.Controllers
{
    /// <summary>
    /// Controller for handling the home page of the application.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Returns the Index view of the Home section.
        /// </summary>
        /// <returns>The Index view of the Home section.</returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Home";
            return View();
        }
    }
}
//-----------------------------------------------------END-OF-FILE-----------------------------------------------------//