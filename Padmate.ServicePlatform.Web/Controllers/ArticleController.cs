using Padmate.ServicePlatform.Web.Models;
using Padmate.ServicePlatform.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Padmate.ServicePlatform.DataAccess;
using Padmate.ServicePlatform.Entities;

namespace Padmate.ServicePlatform.Web.Controllers
{
    public class ArticleController:BaseController
    {
        ServiceDbContext _dbContext = new ServiceDbContext();

        public ActionResult Index()
        {
            //活动预告
            List<Article> activityForecastArticles = _dbContext.Atricles
                .Where(a => a.Type == Common.ActivityForecast)
                .OrderByDescending(a => a.Pubtime)
                .ToList();
            //精彩活动
            List<Article> wonderfulActivityArticles = _dbContext.Atricles
                .Where(a => a.Type == Common.WonderfulActivity)
                .OrderByDescending(a => a.Pubtime)
                .ToList();

            //资讯
            List<Article> informationArticles = _dbContext.Atricles
                .Where(a => a.Type == Common.Information)
                .OrderByDescending(a => a.Pubtime)
                .ToList();


            ViewData["activityForecastArticles"] = activityForecastArticles;
            ViewData["wonderfulActivityArticles"] = wonderfulActivityArticles;
            ViewData["informationArticles"] = informationArticles;


            return View();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="page">当前所在页数</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetPageData(AtricleSearchModel articleSearchModel)
        {
            //每页显示数据条数
            int limits = 5;
            var articleType = articleSearchModel.ArticleType;
            var subTitle = articleSearchModel.SubTitle;
            var currentPage = articleSearchModel.page;

            var query = _dbContext.Atricles
                .Where(a => a.Type == articleType);

            //过滤查询条件
            if (!string.IsNullOrEmpty(subTitle))
                query = query.Where(a=>a.SubTitle.Contains(subTitle));

            //总条数
            var totalCount = query.ToList().Count();
            var totalPages = System.Convert.ToInt32(Math.Ceiling((double)totalCount / limits));

            
            //page:第一页表示从第0条数据开始索引
            Int32 skip = System.Convert.ToInt32((currentPage - 1) * limits);
            //当前分页条数
            var pageData = query.OrderByDescending(a => a.Pubtime)
            .Skip(skip)
            .Take(limits)
            .ToList();

            PageResult<Article> result = new PageResult<Article>(totalPages, pageData);
            return Json(result);
        }
        

        public ActionResult ShowContent(string id)
        {
            
            try
            {
                if (string.IsNullOrEmpty(id))
                    return RedirectToAction("Error", "Home");
                Int32 articleId = System.Convert.ToInt32(id);
                var article = _dbContext.Atricles.FirstOrDefault(a => a.Id == articleId);

                if (article == null)
                    return RedirectToAction("Error", "Home");

                ViewData["article"] = article;

            }catch(Exception e)
            {
                return RedirectToAction("Error","Home");
            }
            
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
        public ActionResult Add(ArticleViewModel model, HttpPostedFileBase articleImage, string ReturnUrl)
        {

            if (ModelState.IsValid && ValideData(model))
            {
                try
                {
                    string virtualUrl = "";
                    if(articleImage != null)
                    {
                        FileInfo articleImageFile = new FileInfo(articleImage.FileName);
                        string saveName = Guid.NewGuid().ToString() + articleImageFile.Extension;
                        string virtualFloder = "../img/Upload/";
                        virtualUrl = Path.Combine(virtualFloder, saveName);
                        string physicleDirectory = Server.MapPath(virtualFloder);
                        string physicalUrl = Path.Combine(physicleDirectory, saveName);
                        if(!System.IO.Directory.Exists(physicleDirectory))
                        {
                            System.IO.Directory.CreateDirectory(physicleDirectory);
                        }
                        articleImage.SaveAs(physicalUrl);

                    }
                    

                    var article = new Article()
                    {
                        Title = model.Title,
                        SubTitle = model.SubTitle,
                        Description = model.Description,
                        ArticleImage = virtualUrl,
                        Content = model.Content,
                        CreateDate = DateTime.Now,
                        Creator = User.Identity.Name,
                        Pubtime =model.Pubtime,
                        Type = model.ArticleType,
                        IsHref = model.IsHref,
                        Href=model.Href
                    };

                    _dbContext.Atricles.Add(article);
                    _dbContext.SaveChanges();
                    //返回文章显示页面
                    return Redirect("/Article/ShowContent?id=" + article.Id.ToString() + "&returnUrl=" + ReturnUrl);

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

            try
            {
                if (string.IsNullOrEmpty(id))
                    return RedirectToAction("Error", "Home");
                Int32 articleId = System.Convert.ToInt32(id);
                var article = _dbContext.Atricles.FirstOrDefault(a => a.Id == articleId);

                if (article == null)
                    return RedirectToAction("Error", "Home");

                ViewData["article"] = article;

            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home");
            }

            return View();
        }

        // POST:
        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(ArticleViewModel model, HttpPostedFileBase articleImage, string ReturnUrl)
        {
            var article = _dbContext.Atricles.FirstOrDefault(a => a.Id == model.Id);
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
                    article.ModifiedDate = DateTime.Now;
                    article.Modifier = User.Identity.Name;
                    article.Pubtime = model.Pubtime;
                    article.IsHref = model.IsHref;
                    article.Href = model.Href;
                    

                    _dbContext.SaveChanges();
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

        private bool ValideData(ArticleViewModel model)
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
            var article = _dbContext.Atricles.FirstOrDefault(a => a.Id == id);
            //删除图标
            //删除原来图片
            if (!string.IsNullOrEmpty(article.ArticleImage))
            {
                if (System.IO.File.Exists(Server.MapPath(article.ArticleImage)))
                    System.IO.File.Delete(Server.MapPath(article.ArticleImage));
            }

            _dbContext.Atricles.Remove(article);
            _dbContext.SaveChanges();
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