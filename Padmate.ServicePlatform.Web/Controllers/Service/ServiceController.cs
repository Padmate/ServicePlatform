using Padmate.ServicePlatform.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Padmate.ServicePlatform.Web.Controllers.Service
{
    public class ServiceController:BaseController
    {
        ServiceDbContext _dbContext = new ServiceDbContext();

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 市场平台
        /// </summary>
        /// <returns></returns>
        public ActionResult Market()
        {
            return View();
        }

        /// <summary>
        /// 产品平台
        /// </summary>
        /// <returns></returns>
        public ActionResult Product()
        {
            return View();
        }

        /// <summary>
        /// 工程平台
        /// </summary>
        /// <returns></returns>
        public ActionResult Engineer()
        {
            return View();
        }

        /// <summary>
        /// 制造平台
        /// </summary>
        /// <returns></returns>
        public ActionResult Make()
        {
            return View();
        }

        /// <summary>
        /// 销售平台
        /// </summary>
        /// <returns></returns>
        public ActionResult Sale()
        {
            return View();
        }

        /// <summary>
        /// 供应链管理
        /// </summary>
        /// <returns></returns>
        public ActionResult SupplyManagement()
        {
            return View();
        }

        /// <summary>
        /// 品牌经营
        /// </summary>
        /// <returns></returns>
        public ActionResult Brand()
        {
            return View();
        }

        /// <summary>
        /// 导师平台
        /// </summary>
        /// <returns></returns>
        public ActionResult Tutor()
        {
            return View();
        }

        /// <summary>
        /// 资金平台
        /// </summary>
        /// <returns></returns>
        public ActionResult Fund()
        {
            return View();
        }
    }
}