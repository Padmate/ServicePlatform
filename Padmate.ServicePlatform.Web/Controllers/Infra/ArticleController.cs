using Padmate.ServicePlatform.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Padmate.ServicePlatform.Models;
using Padmate.ServicePlatform.Service;

namespace Padmate.ServicePlatform.Web.Controllers.Infra
{
    public class ArticleController : BaseController
    {
        string _imageVirtualDirectory = SystemConfig.Init.PathConfiguration["articleThumbnailsVirtualDirectory"].ToString();


        /// <summary>
        /// 查询分页数据
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

            PageResult<M_Article> result = new PageResult<M_Article>(totalCount, totalPages, pageData);
            return Json(result);
        }

        [HttpPost]
        public ActionResult GetArticleById(string articleid)
        {
            B_Article bArticle = new B_Article();

            var article = bArticle.GetArticleById(articleid); ;
            return Json(article);
        }

        public ActionResult Detail(string id)
        {
            B_Article bArticle = new B_Article();

            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("找不到id为空的数据信息");
            }
            var article = bArticle.GetArticleById(id); ;

            ViewData["article"] = article;

            return View();
        }

        /// <summary>
        /// 上传缩略图
        /// </summary>
        /// <param name="model"></param>
        /// <param name="thumbnails"></param>
        /// <param name="ReturnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = SystemRole.SystemAdmin+","+SystemRole.BackstageAdmin)]
        public ActionResult UploadThumbnailsImage(string articleId, HttpPostedFileBase file)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "图片上传成功";

            //如果上传文件不为空
            if (file != null)
            {
                B_Article bArticle = new B_Article();
                B_Image bImage = new B_Image();
                var article = bArticle.GetArticleById(articleId);

                if (article.Image != null)
                {
                    //删除原来的图片
                    message = bImage.DeleteImage(System.Convert.ToInt32(article.Image.Id));
                    if (!message.Success) return Json(message);
                }
                //上传新图标
                message = bImage.AddImage(file, _imageVirtualDirectory, Common.Article_Thumbnails);
                if (!message.Success) return Json(message);
                int imageId = System.Convert.ToInt32(message.ReturnId);
                //更新新图标id到数据库
                message = bArticle.UpdateImageId(articleId, imageId);

            }

            return Json(message);
        }

        public ActionResult Add()
        {
            return View();

        }

        // POST:
        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles = SystemRole.SystemAdmin+","+SystemRole.BackstageAdmin)]
        public ActionResult SaveAdd()
        {
            StreamReader srRequest = new StreamReader(Request.InputStream);
            String strReqStream = srRequest.ReadToEnd();
            M_Article model = JsonHandler.DeserializeJsonToObject<M_Article>(strReqStream);

            Message message = new Message();
            //校验model
            message = ValideData(model);
            if (!message.Success) return Json(message);

            var currentUser = this.GetCurrentUser();
            B_Article bArticle = new B_Article(currentUser);
            message = bArticle.AddArticle(model);

            return Json(message);
        }

        public ActionResult Edit(string articleId)
        {

            B_Article bArticle = new B_Article();
            var article = bArticle.GetArticleById(articleId);
            ViewData["article"] = article;

            return View();
        }

        // POST:
        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles = SystemRole.SystemAdmin+","+SystemRole.BackstageAdmin)]
        public ActionResult SaveEdit()
        {
            StreamReader srRequest = new StreamReader(Request.InputStream);
            String strReqStream = srRequest.ReadToEnd();
            M_Article model = JsonHandler.DeserializeJsonToObject<M_Article>(strReqStream);

            Message message = new Message();
            //校验model
            message = ValideData(model);
            if (!message.Success) return Json(message);

            var currentUser = this.GetCurrentUser();
            B_Article bArticle = new B_Article(currentUser);
            message = bArticle.EditArticle(model);

            return Json(message);

        }


        private Message ValideData(M_Article model)
        {
            Message message = new Message();
            message.Success = true;

            message = model.validate();
            if (!message.Success) return message;

            if (model.IsHref && string.IsNullOrEmpty(model.Href))
            {
                message.Success = false;
                message.Content = "文章链接不能为空";
                return message;
            }
            else if (!model.IsHref && string.IsNullOrEmpty(model.Content))
            {
                message.Success = false;
                message.Content = "文章内容不能为空";
                return message;
            }
            return message;
        }

        [Authorize(Roles = SystemRole.SystemAdmin+","+SystemRole.BackstageAdmin)]
        public ActionResult Delete(string ArticleId)
        {
            B_Article bArticle = new B_Article();
            Message message = bArticle.DeleteArticle(ArticleId);

            return Json(message);
        }

        
    }
}