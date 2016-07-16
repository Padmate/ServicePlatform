using Padmate.ServicePlatform.DataAccess;
using Padmate.ServicePlatform.Entities;
using Padmate.ServicePlatform.Models;
using Padmate.ServicePlatform.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Padmate.ServicePlatform.Service
{
    public class B_Article
    {
        D_Article _dArticle = new D_Article();
        string _imageVirtualDirectory = SystemConfig.Init.PathConfiguration["articleThumbnailsVirtualDirectory"].ToString();

        public B_Article()
        {

        }

        M_User _currentUser;
        string _mapPath;
        public B_Article(M_User currentUser)
        {
            _currentUser = currentUser;

        }

        public B_Article(M_User currentUser,string mapPath)
        {
            _currentUser = currentUser;
            _mapPath = mapPath;

        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public List<M_Article> GetPageData(M_Article article)
        {
            Article searchModel = new Article() {
                Type = article.ArticleType,
                SubTitle = article.SubTitle,
            };
            
            var currentPage = article.page;
            var limit = article.limit;

            //page:第一页表示从第0条数据开始索引
            Int32 skip = System.Convert.ToInt32((currentPage - 1) * limit);

            B_Image bImage = new B_Image();
            var pageResult = _dArticle.GetPageData(searchModel,skip,limit);
            var result = pageResult.Select(a => ConverEntityToModel(a)).ToList();

            return result;
        }

        /// <summary>
        /// 获取分页数据总条数
        /// </summary>
        /// <returns></returns>
        public int GetPageDataTotalCount(M_Article article)
        {
            Article searchModel = new Article()
            {
                Type = article.ArticleType,
                SubTitle = article.SubTitle,
            };

            var totalCount = _dArticle.GetPageDataTotalCount(searchModel);
            return totalCount;
        }

        /// <summary>
        /// 获取活动预告
        /// </summary>
        /// <returns></returns>
        public List<M_Article> GetActivityForecast()
        {
            B_Image bImage = new B_Image();

            var articles = _dArticle.GetArticlesByType(Common.ActivityForecast,null);
            var result = articles.Select(a => ConverEntityToModel(a)).ToList();

            return result ;
        }

        public M_Article GetArticleById(int id)
        {
            B_Image bImage = new B_Image();

            var article = _dArticle.GetArticleById(id);
            var result = ConverEntityToModel(article);
            return result;
        }

        /// <summary>
        /// 获取最新的前三条活动预告
        /// </summary>
        /// <returns></returns>
        public List<M_Article> GetFirstThreeActivityForecast()
        {
            B_Image bImage = new B_Image();

            var articles = _dArticle.GetArticlesByType(Common.ActivityForecast, 3);
            var result = articles.Select(a => ConverEntityToModel(a)).ToList();

            return result;
        }

        /// <summary>
        /// 获取精彩活动
        /// </summary>
        /// <returns></returns>
        public List<M_Article> GetWonderfulActivity()
        {
            var articles = _dArticle.GetArticlesByType(Common.WonderfulActivity, null);
            var result = articles.Select(a =>ConverEntityToModel(a)).ToList();

            return result;
        }

        /// <summary>
        /// 获取最新前三条精彩活动
        /// </summary>
        /// <returns></returns>
        public List<M_Article> GetFirstThreeWonderfulActivity()
        {
            var articles = _dArticle.GetArticlesByType(Common.WonderfulActivity, 3);
            var result = articles.Select(a => ConverEntityToModel(a)).ToList();

            return result;
        }

        /// <summary>
        /// 获取资讯
        /// </summary>
        /// <returns></returns>
        public List<M_Article> GetInformation()
        {
            var articles = _dArticle.GetArticlesByType(Common.Information, null);
            var result = articles.Select(a => ConverEntityToModel(a)).ToList();

            return result;
        }

        /// <summary>
        /// 获取最新前六条资讯
        /// </summary>
        /// <returns></returns>
        public List<M_Article> GetFirstSixInformation()
        {
            var articles = _dArticle.GetArticlesByType(Common.Information, 6);
            var result = articles.Select(a => ConverEntityToModel(a)).ToList();

            return result;
        }

        /// <summary>
        /// 新增文章
        /// </summary>
        /// <param name="model"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public Message AddArticle(M_Article model,HttpPostedFileBase file)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "文章新增成功";

            try
            {
                B_Image bImage = new B_Image();
                message = bImage.AddImage(file, _imageVirtualDirectory, Common.Article_Thumbnails);
                if (!message.Success)
                    return message;
                
                //新增文章
                var article = new Article()
                {
                    Title = model.Title,
                    SubTitle = model.SubTitle,
                    Description = model.Description,
                    ImageId = message.ReturnId,
                    Content = model.Content,
                    CreateDate = DateTime.Now,
                    Creator = _currentUser.UserName,
                    Pubtime = model.Pubtime,
                    Type = model.ArticleType,
                    IsHref = model.IsHref,
                    Href = model.Href
                };

                message.ReturnId= _dArticle.AddAtricle(article);

            }catch(Exception e)
            {
                message.Success = false;
                message.Content = "文章新增失败，异常："+e.Message ;
            }
            return message;
        }

        public Message EditArticle(M_Article model,HttpPostedFileBase file)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "文章修改成功";

            try
            {
                B_Image bImage = new B_Image();
                var articleEntity = _dArticle.GetArticleById(model.Id);
                int? newImageId = articleEntity.ImageId;
                if(file != null)
                {
                    //删除原来的图标
                    if (articleEntity.ImageId != null)
                    {
                        message = bImage.DeleteImage(System.Convert.ToInt32(articleEntity.ImageId));
                        if (!message.Success) return message;
                    }
                    //上传新图标
                    message = bImage.AddImage(file, _imageVirtualDirectory, Common.Article_Thumbnails);
                    if (!message.Success)
                        return message;
                    newImageId = message.ReturnId;
                }

                var article = new Article()
                {
                    Title = model.Title,
                    SubTitle = model.SubTitle,
                    Description = model.Description,
                    ImageId = newImageId,
                    Content = model.Content,
                    CreateDate = DateTime.Now,
                    Creator = _currentUser.UserName,
                    Pubtime = model.Pubtime,
                    Type = model.ArticleType,
                    IsHref = model.IsHref,
                    Href = model.Href
                };

                message.ReturnId = _dArticle.EditArticle(model.Id, article);

            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "文章修改失败，异常：" + e.Message;
            }
            return message;
        }

        

        

        public Message DeleteArticle(int id)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "文章删除成功";
            try
            {
                B_Image bImage = new B_Image();
                //删除文章图标
                var article = _dArticle.GetArticleById(id);
                if(article.ImageId != null)
                    bImage.DeleteImage(System.Convert.ToInt32(article.ImageId));

                _dArticle.DeleteArticle(id);

            }
            catch (Exception e)
            {
                message.Success = false;
                message.Content = "文章删除失败，异常：" + e.Message;
            }
            return message;
        }

        private M_Article ConverEntityToModel(Article article)
        {
            B_Image bImage = new B_Image();

            var model = new M_Article()
            {
                Id = article.Id,
                Title = article.Title,
                SubTitle = article.SubTitle,
                Description = article.Description,
                IsHref = article.IsHref,
                Href = article.Href,
                Content = article.Content,
                Image = article.ImageId == null ? null : bImage.GetImageById(System.Convert.ToInt32(article.ImageId)),
                ArticleType = article.Type,
                Pubtime = article.Pubtime
            };
            return model;
        }

    }
}
