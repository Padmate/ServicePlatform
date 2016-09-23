using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.Entities
{
    /// <summary>
    /// Intel创新项目申请项目申请
    /// </summary>
    public class IntelInnovationProjectApply
    {
        public int Id { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 项目简介
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否有样机
        /// </summary>
        public bool HasExample { get; set; }

        /// <summary>
        /// 创新点
        /// </summary>
        public string InnovationPoint { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactPhone { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<IntelInnovationProjectApplyAttachment> Attachments { get; set; }

        public IntelInnovationProjectApply()
        {
            Attachments = new List<IntelInnovationProjectApplyAttachment>();
        }


    }
}
