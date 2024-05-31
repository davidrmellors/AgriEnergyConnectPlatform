using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgriEnergyConnectPlatform.Controllers
{
    /// <summary>
    /// Controller for the FarmingHub section of the application.
    /// </summary>
    public class FarmingHubController : Controller
    {
        /// <summary>
        /// Returns the Index view of the FarmingHub section.
        /// This action is only accessible to authorized users.
        /// </summary>
        /// <returns>The Index view of the FarmingHub section.</returns>
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}
//-----------------------------------------------------END-OF-FILE-----------------------------------------------------//