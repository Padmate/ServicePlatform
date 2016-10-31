using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        /// 组织名称
        /// 企业名称/团队名称
        /// </summary>
        public string OrganizationName { get; set; }

        /// <summary>
        /// 领域范围
        /// </summary>
        public string FieldScopeCode { get; set; }

        /// <summary>
        /// 领域范围
        /// </summary>
        public string FieldScopeName { get; set; }

        /// <summary>
        /// 项目阶段
        /// </summary>
        public string ProjectStage { get; set; }

        /// <summary>
        /// 成立时间
        /// </summary>
        public DateTime? FoundedTime { get; set; }

        /// <summary>
        /// 营业执照注册号
        /// </summary>
        public string BusinessLicense { get; set; }

        /// <summary>
        /// 办公地址
        /// </summary>
        public string BusinessAddress { get; set; }

        /// <summary>
        /// 网址
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// 微信公众号
        /// </summary>
        public string WebChatNumber { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public string Principal { get; set; }

        /// <summary>
        /// 负责人职位
        /// </summary>
        public string PrincipalPosition { get; set; }

        /// <summary>
        /// 负责人联系电话
        /// </summary>
        public string PrincipalPhone { get; set; }

        /// <summary>
        /// 负责人联系邮箱
        /// </summary>
        public string PrincipalMail { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 联系人职位
        /// </summary>
        public string ContactPosition { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        public string ContactPhone { get; set; }

        /// <summary>
        /// 联系人邮箱
        /// </summary>
        public string ContactMail { get; set; }

        /// <summary>
        /// 组织概要
        /// </summary>
        public string OrganizationDescription { get; set; }

        /// <summary>
        /// 核心技术
        /// </summary>
        public string CoreTechnology { get; set; }

        /// <summary>
        /// 关键词
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 项目简介
        /// </summary>
        public string Description { get; set; }

        #region 投票
        /// <summary>
        /// 投票编号
        /// </summary>
        public string VoteNo { get; set; }

        /// <summary>
        /// 总票数
        /// 默认为0
        /// </summary>
        [DefaultValue(0)]
        public int TotalVotes { get; set; }

        /// <summary>
        /// 并发控制
        /// </summary>
        [Timestamp]
        public byte[] RowVersion { get; set; }


        #endregion

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
        /// 组织名称
        /// 企业名称/团队名称
        /// </summary>
        public string OrganizationName { get; set; }

        /// <summary>
        /// 领域范围
        /// </summary>
        public string FieldScopeCode { get; set; }

        /// <summary>
        /// 领域范围
        /// </summary>
        public string FieldScopeName { get; set; }

        /// <summary>
        /// 项目阶段
        /// </summary>
        public string ProjectStage { get; set; }

        /// <summary>
        /// 成立时间
        /// </summary>
        public DateTime? FoundedTime { get; set; }

        /// <summary>
        /// 营业执照注册号
        /// </summary>
        public string BusinessLicense { get; set; }

        /// <summary>
        /// 办公地址
        /// </summary>
        public string BusinessAddress { get; set; }

        /// <summary>
        /// 网址
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// 微信公众号
        /// </summary>
        public string WebChatNumber { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public string Principal { get; set; }

        /// <summary>
        /// 负责人职位
        /// </summary>
        public string PrincipalPosition { get; set; }

        /// <summary>
        /// 负责人联系电话
        /// </summary>
        public string PrincipalPhone { get; set; }

        /// <summary>
        /// 负责人联系邮箱
        /// </summary>
        public string PrincipalMail { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 联系人职位
        /// </summary>
        public string ContactPosition { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        public string ContactPhone { get; set; }

        /// <summary>
        /// 联系人邮箱
        /// </summary>
        public string ContactMail { get; set; }

        /// <summary>
        /// 组织概要
        /// </summary>
        public string OrganizationDescription { get; set; }

        /// <summary>
        /// 核心技术
        /// </summary>
        public string CoreTechnology { get; set; }

        /// <summary>
        /// 关键词
        /// </summary>
        public string Keyword { get; set; }

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

        public string VoteNo { get; set; }
        public int TotalVotes { get; set; }

        public string Search { get; set; }
    }

}
