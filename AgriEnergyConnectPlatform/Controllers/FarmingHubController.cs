﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgriEnergyConnectPlatform.Controllers
{
    public class FarmingHubController : Controller
    {
        // GET: FarmingHub
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}