using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Padmate.ServicePlatform.Entities
{
    public class Article
    {
        public int Id { get; set; }


        /// <summary>
        /// 文章标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 文章副标题
        /// </summary>
        public string SubTitle { get; set; }

        /// <summary>
        /// 文章描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 模块类型:AtricleType
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 文章是否是链接：
        /// 如果是链接则只Href
        /// 如果不是，则有Content
        /// </summary>
        public bool IsHref { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// 文章内容
        /// </summary>
        [Column("Content", TypeName = "ntext")]
        public string Content { get; set; }

        /// <summary>
        /// 文章图片url
        /// </summary>
        public string ArticleImage { get; set; }


        /// <summary>
        /// 文章创建者
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 文章创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 文章修改者
        /// </summary>
        public string Modifier { get; set; }

        /// <summary>
        /// 文章修改时间
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// 文章发表时间
        /// </summary>
        public DateTime Pubtime { get; set; }

    }



}