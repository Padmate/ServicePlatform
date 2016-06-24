using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Padmate.ServicePlatform.Web.Models
{
    public class ArticleViewModel
    {
        public Int32 Id { get; set; }

        /// <summary>
        /// 文章标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 文章副标题
        /// </summary>
        [Required(ErrorMessage="二级标题不能为空")]
        public string SubTitle { get; set; }

        /// <summary>
        /// 文章描述
        /// </summary>
        public string Description { get; set; }

        public bool IsHref { get; set; }

        public string Href { get; set; }

        /// <summary>
        /// 文章内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 文章图片url
        /// </summary>
        public string ArticleImage { get; set; }


        /// <summary>
        /// 文章发表时间
        /// </summary>
        [Required(ErrorMessage = "发布时间不能为空")]
        public DateTime Pubtime { get; set; }


        public string ArticleType { get; set; }
    }

    public class AtricleSearchModel : BaseModel
    {
        public string ArticleType { get; set; }
        public string SubTitle { get; set; }
    }
}