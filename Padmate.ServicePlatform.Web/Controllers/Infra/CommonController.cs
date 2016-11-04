using Padmate.ServicePlatform.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Padmate.ServicePlatform.Web.Controllers.Infra
{
    public class CommonController:BaseController
    {
        public ActionResult GetAuditStatusDic()
        {

            return Json(Common.Dic_Audit);
        }
    }
}