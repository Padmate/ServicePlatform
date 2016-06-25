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
            var totalCount = bArticle.GetPageDataTotalCount(searchModel.ArticleType);
            //总页数
            var totalPages = System.Convert.ToInt32(Math.Ceiling((double)totalCount / limit));

            PageResult<M_Article> result = new PageResult<M_Article>(totalPages, pageData);
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

        string articleImageVirtualFloader = "../img/Upload/";
         // POST:
        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles="Admin")]
        public ActionResult Add(M_Article model, HttpPostedFileBase articleImage, string ReturnUrl)
        {

            if (ModelState.IsValid && ValideData(model))
            {
                try
                {
                    var mapPath = Server.MapPath("");
                    FileUpload fileUpload = new FileUpload(mapPath);

                    string virtualSavePath = "";
                    if(articleImage != null)
                    {
                        FileInfo articleImageFile = new FileInfo(articleImage.FileName);
                        var physicalSavePath = fileUpload.ConstructPhysicalSavePath(articleImageVirtualFloader
                            ,articleImage.FileName,articleImageFile.Extension);
                        virtualSavePath = fileUpload.ConstructVirtualSavePath(articleImageVirtualFloader
                            , articleImage.FileName, articleImageFile.Extension);

                        articleImage.SaveAs(physicalSavePath);

                    }
                    model.ArticleImage = virtualSavePath;

                    var currentUser = this.GetCurrentUser();
                    B_Article bArticle = new B_Article(currentUser);
                    Message message = bArticle.AddArticle(model);
                    if (!message.Success)
                    {
                        ModelState.AddModelError("", message.Content);
                        return View(model);
                    }
                    
                    //返回文章显示页面
                    return Redirect("/Article/ShowContent?id=" + message.ReturnId + "&returnUrl=" + ReturnUrl);

                }catch(Exception e)
                {
                    ModelState.AddModelError("", "发布异常:"+e.Message);
                    return View(model);
                }
                
            }
            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View(model);
        }  

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string id)
        {

            Int32 articleId = System.Convert.ToInt32(id);

            B_Article bArticle = new B_Article();
            var article = bArticle.GetArticleById(articleId);
            ViewData["article"] = article;

            return View();
        }

        // POST:
        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(M_Article model, HttpPostedFileBase articleImage, string ReturnUrl)
        {
            B_Article bArticle = new B_Article();
            var article = bArticle.GetArticleById(model.Id);
            ViewData["article"] = article;
            if (ModelState.IsValid && ValideData(model))
            {
                try
                {


                    string virtualUrl = article.ArticleImage;
                    if (articleImage != null)
                    {
                        //保存图片
                        FileInfo articleImageFile = new FileInfo(articleImage.FileName);
                        string saveName = Guid.NewGuid().ToString() + articleImageFile.Extension;
                        string virtualFloder = "../img/Upload/";
                        virtualUrl = Path.Combine(virtualFloder, saveName);
                        string physicleDirectory = Server.MapPath(virtualFloder);
                        string physicalUrl = Path.Combine(physicleDirectory, saveName);
                        if (!System.IO.Directory.Exists(physicleDirectory))
                        {
                            System.IO.Directory.CreateDirectory(physicleDirectory);
                        }
                        articleImage.SaveAs(physicalUrl);


                        //删除原来图片
                        if (!string.IsNullOrEmpty(article.ArticleImage))
                        {
                            if (System.IO.File.Exists(Server.MapPath(article.ArticleImage)))
                                System.IO.File.Delete(Server.MapPath(article.ArticleImage));
                        }

                    }


                    article.Title = model.Title;
                    article.SubTitle = model.SubTitle;
                    article.Description = model.Description;
                    article.ArticleImage = virtualUrl;
                    article.Content = model.Content;
                    //article.ModifiedDate = DateTime.Now;
                    //article.Modifier = User.Identity.Name;
                    //article.Pubtime = model.Pubtime;
                    //article.IsHref = model.IsHref;
                    //article.Href = model.Href;
                    

                    //_dbContext.SaveChanges();
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
        public ActionResult Delete(Int32 id,string returnUrl)
        {
            //var article = _dbContext.Atricles.FirstOrDefault(a => a.Id == id);
            ////删除图标
            ////删除原来图片
            //if (!string.IsNullOrEmpty(article.ArticleImage))
            //{
            //    if (System.IO.File.Exists(Server.MapPath(article.ArticleImage)))
            //        System.IO.File.Delete(Server.MapPath(article.ArticleImage));
            //}

            //_dbContext.Atricles.Remove(article);
            //_dbContext.SaveChanges();
            return Redirect(returnUrl); 
        }

        [HttpPost]
        public ActionResult CKEditorUpload(HttpPostedFileBase upload)
        {
            var fileName = System.IO.Path.GetFileName(upload.FileName);
            var filePhysicalDirectory =Server.MapPath("~/img/Upload/images/");
            var filePhysicalPath = Path.Combine(filePhysicalDirectory,fileName); ;//我把它保存在网站根目录的 upload 文件夹

            //如果没有文件夹，则先新建文件夹
            if (!System.IO.Directory.Exists(filePhysicalDirectory))
            {
                System.IO.Directory.CreateDirectory(filePhysicalDirectory);

            }

            upload.SaveAs(filePhysicalPath);

            var url = "/img/Upload/images/" + fileName;
            var CKEditorFuncNum = System.Web.HttpContext.Current.Request["CKEditorFuncNum"];

            //上传成功后，我们还需要通过以下的一个脚本把图片返回到第一个tab选项
            return Content("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + url + "\");</script>");
        }
    }
}