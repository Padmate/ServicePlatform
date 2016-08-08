using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.Models
{
    public class M_ProjectDownload:BaseModel
    {

        public string Id { get; set; }

        /// <summary>
        /// 所属项目的ID
        /// </summary>
        public string ProjectId { get; set; }

        /// <summary>
        /// 下载描述
        /// </summary>
        [Required(ErrorMessage = "附件描述不能为空")]        
        [MaxLength(2000, ErrorMessage = "附件描述不能超过2000个字符")]        
        public string Description { get; set; }

        /// <summary>
        /// 下载虚拟路径
        /// </summary>
        public string VirtualPath { get; set; }

        /// <summary>
        /// 下载物理路径
        /// </summary>
        public string PhysicalPath { get; set; }

        /// <summary>
        /// 下载文件保存名称
        /// </summary>
        [MaxLength(50, ErrorMessage = "文件名不能超过50个字符")]     
        public string SaveName { get; set; }

        /// <summary>
        /// 下载文件后缀
        /// </summary>
        [MaxLength(10, ErrorMessage = "文件后缀不能超过10个字符")]  
        public string Extension { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        [RegularExpression(@"^[1-9]\d*|0$", ErrorMessage = "顺序只能是0或正整数")]
        public string Sequence { get; set; }

        /// <summary>
        /// 缩略图
        /// </summary>
        public M_Image Image { get; set; }
    }
}
