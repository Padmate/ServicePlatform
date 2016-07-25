using Padmate.ServicePlatform.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Padmate.ServicePlatform.Models;
using Padmate.ServicePlatform.Service;

namespace Padmate.ServicePlatform.Web.Controllers
{
    public class InformationController:BaseController
    {


        public ActionResult Index()
        {
            //获取最新前6条资讯信息
            B_Article bArticle = new B_Article();
            var firstSixInformations = bArticle.GetFirstSixInformation();
            ViewData["FirstSixInformations"] = firstSixInformations;

            return View();
        }

        public ActionResult Detail(string id)
        {
            B_Article bArticle = new B_Article();

            Int32 articleId = System.Convert.ToInt32(id);
            var article = bArticle.GetArticleById(articleId); ;

            ViewData["article"] = article;

            //根据id查找上一篇，下一篇记录
            M_Article previousArticle = bArticle.GetPreviousIdByCurrentId(articleId);
            M_Article nextArticle = bArticle.GetNextIdByCurrentId(articleId);
            ViewData["PreviousArticle"] = previousArticle;
            ViewData["NextArticle"] = nextArticle;

            return View();
        }

    }
}