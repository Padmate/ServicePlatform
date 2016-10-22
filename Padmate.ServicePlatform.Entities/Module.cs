using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.Entities
{
    public class Module
    {
        public int Id { get; set; }

        /// <summary>
        /// 模块URL唯一标识
        /// </summary>
        public string ModuleURLId { get; set; }

        /// <summary>
        /// 模块标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 模块二级标题
        /// </summary>
        public string SubTitle { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 模块类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 模块是否是链接：
        /// 如果是链接则只Href
        /// 如果不是，则有Content
        /// </summary>
        public bool IsHref { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// 模块内容
        /// </summary>
        [Column("Content", TypeName = "ntext")]
        public string Content { get; set; }

        /// <summary>
        /// 图片ID
        /// </summary>
        public int? ImageId { get; set; }

        /// <summary>
        /// 模块顺序
        /// </summary>
        public int Sequence { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        public string Modifier { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
    }
}
