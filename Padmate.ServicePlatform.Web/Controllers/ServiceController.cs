using Padmate.ServicePlatform.Models;
using Padmate.ServicePlatform.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Padmate.ServicePlatform.Web.Controllers
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
        public ActionResult MarketPlatform()
        {
            return View();
        }

        /// <summary>
        /// 产品平台
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductPlatform()
        {
            return View();
        }

        /// <summary>
        /// 工程平台
        /// </summary>
        /// <returns></returns>
        public ActionResult ProjectPlatform()
        {
            return View();
        }

        /// <summary>
        /// 制造平台
        /// </summary>
        /// <returns></returns>
        public ActionResult MakePlatform()
        {
            return View();
        }

        /// <summary>
        /// 销售平台
        /// </summary>
        /// <returns></returns>
        public ActionResult SellPlatform()
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
        public ActionResult BrandPlatform()
        {
            return View();
        }

        /// <summary>
        /// 导师平台
        /// </summary>
        /// <returns></returns>
        public ActionResult TutorPlatform()
        {
            return View();
        }

        /// <summary>
        /// 资金平台
        /// </summary>
        /// <returns></returns>
        public ActionResult FundPlatform()
        {
            return View();
        }
    }
}