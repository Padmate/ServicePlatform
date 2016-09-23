using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.Models
{
    public class M_IntelInnovationProjectApplyAttachment:BaseModel
    {
        public string Id { get; set; }

        /// <summary>
        /// 所属项目ID
        /// </summary>
        public string IntelInnovationProjectApplyId { get; set; }


        /// <summary>
        /// 下载虚拟路径
        /// </summary>
        public string VirtualPath { get; set; }

        /// <summary>
        /// 下载物理路径
        /// </summary>
        public string PhysicalPath { get; set; }

        /// <summary>
        /// 附件原始文件名称
        /// </summary>
        [MaxLength(200, ErrorMessage = "原始文件名不能超过200个字符")]
        public string Name { get; set; }

        /// <summary>
        /// 附件原始保存名称
        /// </summary>
        [MaxLength(200, ErrorMessage = "文件名不能超过200个字符")]
        public string SaveName { get; set; }

        /// <summary>
        /// \文件后缀
        /// </summary>
        [MaxLength(50, ErrorMessage = "文件后缀不能超过50个字符")]
        public string Extension { get; set; }
    }
}
