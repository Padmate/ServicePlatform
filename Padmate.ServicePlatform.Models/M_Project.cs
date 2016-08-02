using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.Models
{
    public class M_Project:BaseModel
    {
        public string Id { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [Required(ErrorMessage="项目名称不能为空")]
        public string Name { get; set; }

        /// <summary>
        /// 项目简介/描述
        /// </summary>
        [MaxLength(2000, ErrorMessage = "项目简介不能超过2000个字符")]
        public string Description { get; set; }

        /// <summary>
        /// 项目具体内容/具体描述
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        ///项目类型：属于众创项目 ，还是其它项目 
        /// </summary>
        [Required(ErrorMessage = "项目类型不能为空")]
        [MaxLength(50, ErrorMessage = "项目类型不能超过50个字符")]
        public string Type { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        [RegularExpression(@"^[1-9]\d*|0$", ErrorMessage = "顺序只能是0或正整数")]
        public string Sequence { get; set; }

        /// 创建人
        /// </summary>
        [MaxLength(50, ErrorMessage = "项目创建人不能超过50个字符")]
        public string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        [MaxLength(50, ErrorMessage = "项目修改人不能超过50个字符")]
        public string Modifier { get; set; }

        /// <summary>
        ///修改时间
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// 缩略图
        /// </summary>
        public M_Image Image { get; set; }

        /// <summary>
        /// 项目下载列表
        /// </summary>
        public List<M_ProjectDownload> ProjectDownloads { get; set; }
    }
}
