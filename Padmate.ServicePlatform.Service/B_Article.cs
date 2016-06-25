using Padmate.ServicePlatform.DataAccess;
using Padmate.ServicePlatform.Entities;
using Padmate.ServicePlatform.Models;
using Padmate.ServicePlatform.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.Service
{
    public class B_Article
    {
        D_Article _dArticle = new D_Article();

        public B_Article()
        {

        }

        UserInfo _currentUser;
        public B_Article(UserInfo currentUser)
        {
            _currentUser = currentUser;

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

            var pageResult = _dArticle.GetPageData(searchModel,skip,limit);
            var result = pageResult.Select(a => new M_Article()
            {
                Id = a.Id,
                Title = a.Title,
                SubTitle = a.SubTitle,
                Description = a.Description,
                IsHref = a.IsHref,
                Href = a.Href,
                Content = a.Content,
                ArticleImage = a.ArticleImage,
                ArticleType = a.Type,
                Pubtime = a.Pubtime
            }).ToList();

            return result;
        }

        /// <summary>
        /// 获取分页数据总条数
        /// </summary>
        /// <returns></returns>
        public int GetPageDataTotalCount(string articleType)
        {
            var articles = _dArticle.GetArticlesByType(articleType,null);
            var totalCount = articles.Count();
            return totalCount;
        }

        /// <summary>
        /// 获取活动预告
        /// </summary>
        /// <returns></returns>
        public List<M_Article> GetActivityForecast()
        {
            var articles = _dArticle.GetArticlesByType(Common.ActivityForecast,null);
            var result = articles.Select(a => new M_Article()
                {
                    Id = a.Id,
                    Title = a.Title,
                    SubTitle = a.SubTitle,
                    Description = a.Description,
                    IsHref = a.IsHref,
                    Href = a.Href,
                    Content = a.Content,
                    ArticleImage = a.ArticleImage,
                    ArticleType = a.Type,
                    Pubtime = a.Pubtime
                })
                .ToList();

            return result ;
        }

        public M_Article GetArticleById(int id)
        {
            var article = _dArticle.GetArticleById(id);
            var result = new M_Article() {
                Id = article.Id,
                Title = article.Title,
                SubTitle = article.SubTitle,
                Description = article.Description,
                IsHref = article.IsHref,
                Href = article.Href,
                Content = article.Content,
                ArticleImage = article.ArticleImage,
                ArticleType = article.Type,
                Pubtime = article.Pubtime
            };
            return result;
        }

        /// <summary>
        /// 获取最新的前三条活动预告
        /// </summary>
        /// <returns></returns>
        public List<M_Article> GetFirstThreeActivityForecast()
        {
            var articles = _dArticle.GetArticlesByType(Common.ActivityForecast, 3);
            var result = articles.Select(a => new M_Article()
            {
                Id = a.Id,
                Title = a.Title,
                SubTitle = a.SubTitle,
                Description = a.Description,
                IsHref = a.IsHref,
                Href = a.Href,
                Content = a.Content,
                ArticleImage = a.ArticleImage,
                ArticleType = a.Type,
                Pubtime = a.Pubtime
            })
                .ToList();

            return result;
        }

        /// <summary>
        /// 获取精彩活动
        /// </summary>
        /// <returns></returns>
        public List<M_Article> GetWonderfulActivity()
        {
            var articles = _dArticle.GetArticlesByType(Common.WonderfulActivity, null);
            var result = articles.Select(a => new M_Article()
            {
                Id = a.Id,
                Title = a.Title,
                SubTitle = a.SubTitle,
                Description = a.Description,
                IsHref = a.IsHref,
                Href = a.Href,
                Content = a.Content,
                ArticleImage = a.ArticleImage,
                ArticleType = a.Type,
                Pubtime = a.Pubtime
            })
                .ToList();

            return result;
        }

        /// <summary>
        /// 获取最新前三条精彩活动
        /// </summary>
        /// <returns></returns>
        public List<M_Article> GetFirstThreeWonderfulActivity()
        {
            var articles = _dArticle.GetArticlesByType(Common.WonderfulActivity, 3);
            var result = articles.Select(a => new M_Article()
            {
                Id = a.Id,
                Title = a.Title,
                SubTitle = a.SubTitle,
                Description = a.Description,
                IsHref = a.IsHref,
                Href = a.Href,
                Content = a.Content,
                ArticleImage = a.ArticleImage,
                ArticleType = a.Type,
                Pubtime = a.Pubtime
            })
                .ToList();

            return result;
        }

        /// <summary>
        /// 获取资讯
        /// </summary>
        /// <returns></returns>
        public List<M_Article> GetInformation()
        {
            var articles = _dArticle.GetArticlesByType(Common.Information, null);
            var result = articles.Select(a => new M_Article()
            {
                Id = a.Id,
                Title = a.Title,
                SubTitle = a.SubTitle,
                Description = a.Description,
                IsHref = a.IsHref,
                Href = a.Href,
                Content = a.Content,
                ArticleImage = a.ArticleImage,
                ArticleType = a.Type,
                Pubtime = a.Pubtime
            })
                .ToList();

            return result;
        }

        /// <summary>
        /// 获取最新前六条资讯
        /// </summary>
        /// <returns></returns>
        public List<M_Article> GetFirstSixInformation()
        {
            var articles = _dArticle.GetArticlesByType(Common.Information, 6);
            var result = articles.Select(a => new M_Article()
            {
                Id = a.Id,
                Title = a.Title,
                SubTitle = a.SubTitle,
                Description = a.Description,
                IsHref = a.IsHref,
                Href = a.Href,
                Content = a.Content,
                ArticleImage = a.ArticleImage,
                ArticleType = a.Type,
                Pubtime = a.Pubtime
            })
                .ToList();

            return result;
        }

        public Message AddArticle(M_Article model)
        {
            Message message = new Message();
            message.Success = true;
            message.Content = "文章新增成功";

            try
            {
                var article = new Article()
                {
                    Title = model.Title,
                    SubTitle = model.SubTitle,
                    Description = model.Description,
                    ArticleImage = model.ArticleImage,
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
    }
}
