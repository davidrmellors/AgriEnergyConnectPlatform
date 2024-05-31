using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgriEnergyConnectPlatform.Controllers
{
    /// <summary>
    /// Controller for managing projects.
    /// </summary>
    public class ProjectsController : Controller
    {
        /// <summary>
        /// Returns the Index view of the Projects section.
        /// </summary>
        /// <returns>The Index view of the Projects section.</returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}
//-----------------------------------------------------END-OF-FILE-----------------------------------------------------//