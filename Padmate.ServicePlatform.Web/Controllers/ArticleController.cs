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
    public class ArticleController:BaseController
    {
       
        public ActionResult Index()
        {
            return View();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="page">当前所在页数</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetPageData(M_Article searchModel)
        {
            //每页显示数据条数
            int limit = 5;
            searchModel.limit = limit;

            B_Article bArticle = new B_Article();
            var pageData = bArticle.GetPageData(searchModel);
            var totalCount = bArticle.GetPageDataTotalCount(searchModel);
            //总页数
            var totalPages = System.Convert.ToInt32(Math.Ceiling((double)totalCount / limit));

            PageResult<M_Article> result = new PageResult<M_Article>(totalCount,totalPages, pageData);
            return Json(result);
        }
        

        public ActionResult ShowContent(string id)
        {
            B_Article bArticle = new B_Article();

            Int32 articleId = System.Convert.ToInt32(id);
            var article = bArticle.GetArticleById(articleId); ;

            ViewData["article"] = article;

            return View();
        }

        [HttpGet]
        [Authorize(Roles="Admin")]
        public ActionResult Add()
        {
            return View();
        }

         // POST:
        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles="Admin")]
        public ActionResult Add(M_Article model, HttpPostedFileBase thumbnails, string ReturnUrl)
        {
            if (ModelState.IsValid && ValideData(model))
            {
                var currentUser = this.GetCurrentUser();
                B_Article bArticle = new B_Article(currentUser);
                Message message = bArticle.AddArticle(model,thumbnails);
                if (!message.Success)
                {
                    ModelState.AddModelError("", message.Content);
                    return View(model);
                }
                    
                //返回文章显示页面
                return Redirect("/Article/ShowContent?id=" + message.ReturnId + "&returnUrl=" + ReturnUrl);

            }
            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View(model);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string ArticleId)
        {

            Int32 articleId = System.Convert.ToInt32(ArticleId);

            B_Article bArticle = new B_Article();
            var article = bArticle.GetArticleById(articleId);
            ViewData["article"] = article;

            return View();
        }

        // POST:
        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(M_Article model, HttpPostedFileBase thumbnails, string ReturnUrl)
        {
            var currentUser = this.GetCurrentUser();
            B_Article bArticle = new B_Article(currentUser);
            var article = bArticle.GetArticleById(model.Id);
            ViewData["article"] = article;

            if (ModelState.IsValid && ValideData(model))
            {
                try
                {
                    Message message = bArticle.EditArticle(model,thumbnails);
                    if (!message.Success)
                    {
                        ModelState.AddModelError("", message.Content);
                        return View(model);
                    }
                    //返回文章显示页面
                    return Redirect("/Article/ShowContent?id=" + article.Id.ToString() + "&returnUrl=" + ReturnUrl);

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "发布异常:" + e.Message);
                    return View(model);
                }

            }
            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View(model);
        }


        private bool ValideData(M_Article model)
        {
            if (model.IsHref && string.IsNullOrEmpty(model.Href))
            {
                ModelState.AddModelError("", "文章链接不能为空");
                return false;
            }
            else if (!model.IsHref && string.IsNullOrEmpty(model.Content))
            {
                ModelState.AddModelError("", "文章内容不能为空");
                return false;
            }
            return true;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string ArticleId)
        {
            Int32 articleId = System.Convert.ToInt32(ArticleId);
            B_Article bArticle = new B_Article();
            Message message = bArticle.DeleteArticle(articleId);

            return Json(message); 
        }

        [HttpPost]
        public ActionResult CKEditorUpload(HttpPostedFileBase upload)
        {
            var viirtualPath = SystemConfig.Init.PathConfiguration["articleContentImageVirtualDirectory"].ToString();
            var fileName = System.IO.Path.GetFileName(upload.FileName);
            var filePhysicalDirectory = Server.MapPath("~" + viirtualPath);
            var filePhysicalPath = Path.Combine(filePhysicalDirectory,fileName); ;//我把它保存在网站根目录的 upload 文件夹

            //如果没有文件夹，则先新建文件夹
            if (!System.IO.Directory.Exists(filePhysicalDirectory))
            {
                System.IO.Directory.CreateDirectory(filePhysicalDirectory);

            }

            upload.SaveAs(filePhysicalPath);

            var url = Path.Combine(viirtualPath,fileName);
            var CKEditorFuncNum = System.Web.HttpContext.Current.Request["CKEditorFuncNum"];

            //上传成功后，我们还需要通过以下的一个脚本把图片返回到第一个tab选项
            return Content("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + url + "\");</script>");
        }
    }
}