using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Padmate.ServicePlatform.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SystemManageController:BaseController
    {
        /// <summary>
        /// 首页图片管理
        /// </summary>
        /// <returns></returns>
        public ActionResult HomeBackground()
        {

            return View();
        }

        #region 服务管理
        /// <summary>
        /// 产品平台
        /// </summary>
        /// <returns></returns>
        public ActionResult ServiceProduct()
        {

            return View();
        }

        #endregion


        #region 用户管理
        public ActionResult UserManage()
        {
            return View();
        }
        #endregion
    }
}