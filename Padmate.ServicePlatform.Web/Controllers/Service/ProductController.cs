using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Padmate.ServicePlatform.Web.Controllers.Service
{
    public class ProductController:BaseController
    {
        /// <summary>
        /// 众创项目
        /// </summary>
        /// <returns></returns>
        public ActionResult ZZProject()
        {
            return View();
        }

        /// <summary>
        /// 产学研 industrial-academic-research
        /// </summary>
        /// <returns></returns>
        public ActionResult IAR()
        {
            return View();
        }

        /// <summary>
        /// 文化创意
        /// </summary>
        /// <returns></returns>
        public ActionResult CulturalCreative()
        {
            return View();
        }

        /// <summary>
        /// IDMD
        /// </summary>
        /// <returns></returns>
        public ActionResult IDMD()
        {
            return View();
        }

        /// <summary>
        /// 硬件设计
        /// </summary>
        /// <returns></returns>
        public ActionResult HardwareDesign()
        {
            return View();
        }

        /// <summary>
        /// 软件开发
        /// </summary>
        /// <returns></returns>
        public ActionResult SoftwareDesign()
        {
            return View();

        }

        /// <summary>
        /// 客户需求
        /// </summary>
        /// <returns></returns>
        public ActionResult CustomerRequirement()
        {
            return View();
        }

        /// <summary>
        /// 产品众筹
        /// </summary>
        public ActionResult ProductCrowdfunding()
        {
            return View();

        }

        /// <summary>
        /// ODM OEM
        /// </summary>
        /// <returns></returns>
        public ActionResult OdmOem()
        {
            return View();
        }

    }
}