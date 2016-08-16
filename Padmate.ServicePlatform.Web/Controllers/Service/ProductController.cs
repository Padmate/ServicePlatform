using Padmate.ServicePlatform.Service;
using Padmate.ServicePlatform.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Padmate.ServicePlatform.Web.Controllers.Service
{
    public class ProductController:BaseController
    {
        #region
        /// <summary>
        /// 众创项目
        /// </summary>
        /// <returns></returns>
        public ActionResult ZZProject()
        {
            //查找众创项目
            B_Project bProject = new B_Project();
            var zzProjects = bProject.GetProjectByType(Common.ZZ_Project);

            //查找其它项目
            var otherProjects = bProject.GetProjectByType(Common.Other_Project);

            ViewData["zzprojects"] = zzProjects;
            ViewData["otherprojcets"] = otherProjects;

            return View();
        }

        /// <summary>
        /// 众创项目详细
        /// </summary>
        /// <returns></returns>
        public ActionResult ZZProjectDetail(string id)
        {
            B_Project bProject = new B_Project();
            var project = bProject.GetProjectById(id);

            //如果为空，则返回404
            if (project == null) throw new HttpException(404, "");

            ViewData["project"] = project;

            return View();
        }


        #endregion

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