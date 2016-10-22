using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.Models
{
    public class M_Module:BaseModel
    {
        public int Id { get; set; }

        /// <summary>
        /// 模块URL唯一标识
        /// </summary>
        [Required(ErrorMessage = "URL标识不能为空")]
        [MaxLength(200, ErrorMessage = "URL标识不能超过200个字符")]
        [RegularExpression(@"[A-Za-z0-9-]+", ErrorMessage = "URL标识只能是字母、数字或 - ")]
        public string ModuleURLId { get; set; }

        /// <summary>
        /// 模块标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 模块二级标题
        /// </summary>
        [Required(ErrorMessage = "二级标题不能为空")]
        public string SubTitle { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(200, ErrorMessage = "描述长度不能超过200个字符")]        
        public string Description { get; set; }

        /// <summary>
        /// 模块类型
        /// </summary>
        [Required(ErrorMessage = "类型不能为空")]
        [MaxLength(100, ErrorMessage = "类型不能超过100个字符")]
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
        public string Content { get; set; }

        /// <summary>
        /// 缩略图
        /// </summary>
        public M_Image Image { get; set; }

        /// <summary>
        /// 模块顺序
        /// </summary>
        [RegularExpression(@"^[1-9]\d*|0$", ErrorMessage = "顺序只能是0或正整数")]
        public string Sequence { get; set; }

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
