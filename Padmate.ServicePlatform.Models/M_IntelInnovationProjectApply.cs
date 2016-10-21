using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.Models
{
    public class M_IntelInnovationProjectApply:BaseModel
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        /// <summary>
        /// 组织名称
        /// 企业名称/团队名称
        /// </summary>
        [Required(ErrorMessage ="组织名称不能为空")]
        [MaxLength(1000, ErrorMessage = "组织名称不能超过1000个字符")]        
        public string OrganizationName { get; set; }

        /// <summary>
        /// 领域范围
        /// </summary>
        [Required(ErrorMessage = "领域范围不能为空")]
        [MaxLength(50, ErrorMessage = "领域范围不能超过50个字符")] 
        public string FieldScopeCode { get; set; }

        /// <summary>
        /// 领域范围
        /// </summary>
        [Required(ErrorMessage = "领域范围不能为空")]
        [MaxLength(100, ErrorMessage = "领域范围不能超过100个字符")]
        public string FieldScopeName { get; set; }

        /// <summary>
        /// 项目阶段
        /// </summary>
        [Required(ErrorMessage = "项目阶段不能为空")]
        [MaxLength(50, ErrorMessage = "项目阶段不能超过50个字符")] 
        public string ProjectStage { get; set; }

        /// <summary>
        /// 成立时间
        /// </summary>
        public DateTime? FoundedTime { get; set; }

        /// <summary>
        /// 营业执照注册号
        /// </summary>
        [MaxLength(500, ErrorMessage = "营业执照号不能超过500个字符")] 
        public string BusinessLicense { get; set; }

        /// <summary>
        /// 办公地址
        /// </summary>
        [Required(ErrorMessage = "办公地址不能为空")]
        [MaxLength(2000, ErrorMessage = "办公地址不能超过2000个字符")] 
        public string BusinessAddress { get; set; }

        /// <summary>
        /// 网址
        /// </summary>
        [Required(ErrorMessage = "网址不能为空")]
        [MaxLength(2000, ErrorMessage = "网址不能超过2000个字符")]
        [RegularExpression(@"(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?", ErrorMessage = "网址格式不正确，正确格式如：http://www.domain.com ")]
        public string Website { get; set; }

        /// <summary>
        /// 微信公众号
        /// </summary>
        [Required(ErrorMessage = "微信公众号不能为空")]
        [MaxLength(100, ErrorMessage = "微信公众号不能超过100个字符")] 
        public string WebChatNumber { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        [Required(ErrorMessage = "负责人名称不能为空")]
        [MaxLength(100, ErrorMessage = "负责人名称不能超过100个字符")]
        public string Principal { get; set; }

        /// <summary>
        /// 负责人职位
        /// </summary>
        [Required(ErrorMessage = "负责人职位不能为空")]
        [MaxLength(100, ErrorMessage = "负责人职位不能超过100个字符")] 
        public string PrincipalPosition { get; set; }

        /// <summary>
        /// 负责人联系电话
        /// </summary>
        [Required(ErrorMessage = "负责人联系电话不能为空")]
        [MaxLength(100, ErrorMessage = "负责人联系电话不能超过100个字符")]
        [RegularExpression(@"((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)",ErrorMessage="负责人电话格式不正确。")]
        public string PrincipalPhone { get; set; }

        /// <summary>
        /// 负责人联系邮箱
        /// </summary>
        [Required(ErrorMessage = "负责人联系邮箱不能为空")]
        [MaxLength(100, ErrorMessage = "负责人联系邮箱不能超过100个字符")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "负责人邮件格式不正确")]
        public string PrincipalMail { get; set; }



        /// 联系人
        /// </summary>
        [Required(ErrorMessage = "联系人不能为空")]
        [MaxLength(100, ErrorMessage = "联系人不能超过100个字符")] 
        public string Contact { get; set; }

        /// <summary>
        /// 联系人职位
        /// </summary>
        [Required(ErrorMessage = "联系人职位不能为空")]
        [MaxLength(100, ErrorMessage = "联系人职位不能超过100个字符")] 
        public string ContactPosition { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [Required(ErrorMessage = "联系人电话不能为空")]
        [MaxLength(100, ErrorMessage = "联系人电话不能超过100个字符")]
        [RegularExpression(@"((\d{11})|^((\d{7,8})|(\d{4}|\d{3})-(\d{7,8})|(\d{4}|\d{3})-(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1})|(\d{7,8})-(\d{4}|\d{3}|\d{2}|\d{1}))$)", ErrorMessage = "联系人电话格式不正确。")]
        public string ContactPhone { get; set; }

        /// <summary>
        /// 联系人邮箱
        /// </summary>
        [Required(ErrorMessage = "联系人邮箱不能为空")]
        [MaxLength(100, ErrorMessage = "联系人邮箱不能超过100个字符")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "联系人邮件格式不正确")]
        public string ContactMail { get; set; }

        /// <summary>
        /// 组织概要
        /// </summary>
        [Required(ErrorMessage = "概要不能为空")]
        [MaxLength(1000, ErrorMessage = "概要不能超过1000个字符")] 
        public string OrganizationDescription { get; set; }

        /// <summary>
        /// 核心技术
        /// </summary>
        [Required(ErrorMessage = "核心技术不能为空")]
        [MaxLength(1000, ErrorMessage = "核心技术不能超过1000个字符")] 
        public string CoreTechnology { get; set; }

        /// <summary>
        /// 关键词
        /// </summary>
        [Required(ErrorMessage = "关键词不能为空")]
        [MaxLength(100, ErrorMessage = "关键词不能超过100个字符")] 
        public string Keyword { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [Required(ErrorMessage = "项目名称不能为空")]
        [MaxLength(1000, ErrorMessage = "项目名称不能超过1000个字符")] 
        public string Name { get; set; }

        /// <summary>
        /// 项目简介/描述
        /// </summary>
        [Required(ErrorMessage = "项目简介不能为空")] 
        [MaxLength(1000, ErrorMessage = "项目简介不能超过1000个字符")]
        public string Description { get; set; }
      
        /// <summary>
        /// 附件列表
        /// </summary>
        public List<M_IntelInnovationProjectApplyAttachment> Attachments { get; set; }

        public List<M_IntelInnovationProjectApplyQue> Ques { get; set; }

        /// <summary>
        /// 最新的队列
        /// </summary>
        public M_IntelInnovationProjectApplyQue LatestQue { get; set; }
    }

    public class M_IntelInnovationProjectApplySearch:BaseModel
    {
        public string Id { get; set; }

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

        public string QueId { get; set; }

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
