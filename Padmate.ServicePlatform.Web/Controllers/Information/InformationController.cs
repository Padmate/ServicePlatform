using Padmate.ServicePlatform.Models;
using Padmate.ServicePlatform.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Padmate.ServicePlatform.Web.Controllers.Information
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

            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("找不到id为空的数据信息");
            }
            var article = bArticle.GetArticleById(id); ;

            //如果为空，则返回404
            if (article == null) throw new HttpException(404, "");

            ViewData["article"] = article;

            //根据id查找上一篇，下一篇记录
            M_Article previousArticle = bArticle.GetPreviousIdByCurrentId(id);
            M_Article nextArticle = bArticle.GetNextIdByCurrentId(id);
            ViewData["PreviousArticle"] = previousArticle;
            ViewData["NextArticle"] = nextArticle;

            return View();
        }
    }
}