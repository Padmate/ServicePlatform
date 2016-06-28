using Padmate.ServicePlatform.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.DataAccess
{
    public class D_Article
    {
        ServiceDbContext _dbContext = new ServiceDbContext();

        
        /// <summary>
        /// 根据类型获取文章，返回前limit条数据
        /// </summary>
        /// <param name="articleType"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public List<Article> GetArticlesByType(string articleType,int? limit)
        {
            var queryData = _dbContext.Atricles
                .Where(a => a.Type == articleType)
                .OrderByDescending(a => a.Pubtime);

            if(limit != null)
            {
                var articles = queryData.Take(System.Convert.ToInt32(limit))
                    .ToList();
                return articles;
            }

            return queryData.ToList(); ;
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="article"></param>
        /// <param name="skip"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public List<Article> GetPageData(Article article,int skip,int limit)
        {
            var query = _dbContext.Atricles.Where(a=>1==1);

            #region　条件过滤
            if (!string.IsNullOrEmpty(article.Type))
                query = query.Where(a=>a.Type == article.Type);

            if (!string.IsNullOrEmpty(article.SubTitle))
                query = query.Where(a => a.SubTitle.Contains(article.SubTitle));
            #endregion

            var result = query.OrderByDescending(a => a.Pubtime)
            .Skip(skip)
            .Take(limit)
            .ToList();

            return result;
        }

        /// <summary>
        /// 根据ID查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Article GetArticleById(int id)
        {
            var article = _dbContext.Atricles.FirstOrDefault(a => a.Id == id);
            return article;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public int AddAtricle(Article article)
        {
            _dbContext.Atricles.Add(article);
            _dbContext.SaveChanges();
            return article.Id;
        }

        public int EditArticle(int id,Article model)
        {
            var article = _dbContext.Atricles.FirstOrDefault(a => a.Id == id);

            article.Title = model.Title;
            article.SubTitle = model.SubTitle;
            article.Description = model.Description;
            article.ArticleImage = model.ArticleImage;
            article.Content = model.Content;
            article.ModifiedDate = model.ModifiedDate;
            article.Modifier = model.Modifier;
            article.Pubtime = model.Pubtime;
            article.IsHref = model.IsHref;
            article.Href = model.Href;

            _dbContext.SaveChanges();
            return article.Id;
        }

        public void DeleteArticle(int id)
        {
            var article = _dbContext.Atricles.Where(i => i.Id == id).FirstOrDefault();
            if (article != null)
            {
                _dbContext.Atricles.Remove(article);
                _dbContext.SaveChanges();
            }
        }
    }
}
