using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgriEnergyConnectPlatform.Controllers
{
    /// <summary>
    /// Controller for managing training related actions.
    /// </summary>
    public class TrainingController : Controller
    {
        /// <summary>
        /// Returns the Index view of the Training section.
        /// </summary>
        /// <returns>The Index view of the Training section.</returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}
//-----------------------------------------------------END-OF-FILE-----------------------------------------------------//