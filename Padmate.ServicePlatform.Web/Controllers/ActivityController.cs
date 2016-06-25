using Padmate.ServicePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Padmate.ServicePlatform.Web.Controllers
{
    public class ActivityController:BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}