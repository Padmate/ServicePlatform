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

        public virtual ICollection<IntelInnovationProjectApplyQue> Ques { get; set; }
        public virtual ICollection<IntelInnovationProjectApplyAttachment> Attachments { get; set; }

        public IntelInnovationProjectApply()
        {
            Ques = new List<IntelInnovationProjectApplyQue>();
            Attachments = new List<IntelInnovationProjectApplyAttachment>();
        }


    }

    public class IntelInnovationProjectApplySearch
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

        public int QueId { get; set; }

        /// <summary>
        /// 审核状态:审核中\审核通过\审核失败
        /// </summary>
        public string AuditStatus { get; set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? AuditDate { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string Auditor { get; set; }

        /// <summary>
        /// 审核备注
        /// </summary>
        public string AuditRemark { get; set; }


        /// <summary>
        /// 申请人
        /// </summary>
        public string Application { get; set; }

        /// <summary>
        /// 申请日期
        /// </summary>
        public DateTime ApplicationDate { get; set; }


    }

}
