using Padmate.ServicePlatform.Models;
using Padmate.ServicePlatform.Service;
using Padmate.ServicePlatform.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Padmate.ServicePlatform.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Default()
        {
            ViewBag.From = "Default";

            B_Article _bArticle = new B_Article();
            B_Image _bImage = new B_Image();

            List<M_Article> activityForecastArticles = _bArticle.GetFirstThreeActivityForecast();
            List<M_Article> wonderfulActivityArticles = _bArticle.GetFirstThreeWonderfulActivity();
            List<M_Article> informationArticles = _bArticle.GetFirstSixInformation();

            ViewData["activityForecastArticles"] = activityForecastArticles;
            ViewData["wonderfulActivityArticles"] = wonderfulActivityArticles;
            ViewData["informationArticles"] = informationArticles;

            //首页图片
            List<M_Image> homebgImages = _bImage.GetHomeBGImages();
            ViewData["homebgImages"] = homebgImages;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            B_Contact bContact = new B_Contact();
            B_ContactScope bContactScope = new B_ContactScope();

            var contactScopes = bContactScope.GetAllData();
            var contacts = bContact.GetAllData();
            ViewData["Contacts"] = contacts;
            ViewData["ContactScopes"] = contactScopes;
            
            return View();
        }


        #region 错误页面

        /// <summary>
        /// 500服务器错误
        /// </summary>
        /// <returns></returns>
        public ActionResult Error500()
        {
            return View();
        }

        /// <summary>
        /// 404找不到页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Error404()
        {
            return View();
        }

        #endregion

    }
}