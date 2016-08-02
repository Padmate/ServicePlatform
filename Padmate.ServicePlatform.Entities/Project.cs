using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.Entities
{
    public class Project
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public Guid Id { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 项目简介/描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 项目具体内容/具体描述
        /// </summary>
        [Column("Content", TypeName = "ntext")]
        public string Content { get; set; }
        
        /// <summary>
        ///项目类型：属于众创项目 ，还是其它项目 
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 项目排列顺序
        /// </summary>
        public int Sequence { get; set; }

        /// <summary>
        /// 项目缩略图
        /// </summary>
        public int? ImageId { get; set; }

        /// 创建人
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
        ///修改时间
        /// </summary>
        public DateTime? ModifiedDate { get; set; }


        public virtual ICollection<ProjectDownload> ProjectDownloads { get; set; }

        public Project()
        {
            ProjectDownloads = new List<ProjectDownload>();
        }
    }
}
