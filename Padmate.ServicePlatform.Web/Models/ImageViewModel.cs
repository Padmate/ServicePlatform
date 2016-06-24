using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Padmate.ServicePlatform.Web.Models
{
    public class ImageViewModel
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }

        /// <summary>
        /// 图片排列顺序
        /// </summary>
        public int Sequence { get; set; }

        /// <summary>
        /// 图片所属类型
        /// </summary>
        public string Type { get; set; }
    }
}