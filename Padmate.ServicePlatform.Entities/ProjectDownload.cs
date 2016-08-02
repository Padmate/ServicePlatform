using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.Entities
{
    public class ProjectDownload
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]        
        public Guid Id { get; set; }

        /// <summary>
        /// 所属项目的ID
        /// </summary>
        public Guid ProjectId { get; set; }

        public virtual Project Project { get; set; }

        /// <summary>
        /// 下载描述
        /// </summary>
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
        public string SaveName { get; set; }

        /// <summary>
        /// 下载文件后缀
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// 下载显示顺序
        /// </summary>
        public int Sequence { get; set; }

        /// <summary>
        /// 下载缩略图
        /// </summary>
        public int? ImageId { get; set; }

    }
}
