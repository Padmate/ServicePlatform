using Padmate.ServicePlatform.Models;
using Padmate.ServicePlatform.Service;
using Padmate.ServicePlatform.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Padmate.ServicePlatform.Web.Controllers.Activity
{
    public class ActivityController:BaseController
    {
        public ActionResult Index()
        {
            //查询最新的前3条活动预告
            B_Article bArticle = new B_Article();
            var firstThreeActivities = bArticle.GetFirstThreeActivityForecast();
            ViewData["FirstThreeActivitiyForecast"] = firstThreeActivities;

            //查询最新的前3条精彩活动
            var firstThreeWonderfulActivies = bArticle.GetFirstThreeWonderfulActivity();
            ViewData["FirstThreeWonderfulActivies"] = firstThreeWonderfulActivies;
            return View();
        }


        public ActionResult Detail(string type,string id)
        {
            B_Article bArticle = new B_Article();
            if(string.IsNullOrEmpty(id))
            {
                throw new Exception("找不到id为空的数据信息");
            }

            if(!Common.Dic_ArticleType.ContainsKey(type))
            {
               throw new HttpException(404, "");

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