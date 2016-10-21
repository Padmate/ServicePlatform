using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.Entities
{
    public class UserAttachment
    {
        public int Id { get; set; }
        /// <summary>
        /// 虚拟路径
        /// </summary>
        public string VirtualPath { get; set; }

        /// <summary>
        /// 物理路径
        /// </summary>
        public string PhysicalPath { get; set; }

        /// <summary>
        /// 附件名称(原始名称)
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 附件保存名称
        /// </summary>
        public string SaveName { get; set; }

        /// <summary>
        /// 附件后缀名
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// 附件类型
        /// </summary>
        public string Type { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
