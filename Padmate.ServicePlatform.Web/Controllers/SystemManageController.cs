using Padmate.ServicePlatform.Models;
using Padmate.ServicePlatform.Service;
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
        /// 市场平台
        /// </summary>
        /// <returns></returns>
        public ActionResult ServiceMarket()
        {

            return View();
        }

        /// <summary>
        /// 产品平台
        /// </summary>
        /// <returns></returns>
        public ActionResult ServiceProduct()
        {

            return View();
        }

        /// <summary>
        /// 工程平台
        /// </summary>
        /// <returns></returns>
        public ActionResult ServiceEngineer()
        {

            return View();
        }

        #endregion

        #region 活动管理

        /// <summary>
        /// 精彩活动
        /// </summary>
        /// <returns></returns>
        public ActionResult ActivityForecast()
        {

            return View();
        }

        /// <summary>
        /// 活动预告
        /// </summary>
        /// <returns></returns>
        public ActionResult WonderfulActivity()
        {

            return View();
        }
        #endregion 

        #region 资讯管理

        /// <summary>
        /// 资讯管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Information()
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