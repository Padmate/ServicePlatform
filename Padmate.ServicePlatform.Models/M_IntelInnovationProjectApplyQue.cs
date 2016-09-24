using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.Models
{
    public class M_IntelInnovationProjectApplyQue:BaseModel
    {
        public string Id { get; set; }

        /// <summary>
        /// 所属项目ID
        /// </summary>
        public string IntelInnovationProjectApplyId { get; set; }


        /// <summary>
        /// 审核人
        /// </summary>
        [Required(ErrorMessage="审核人不能为空")]
        [MaxLength(200, ErrorMessage = "审核人不能超过200个字符")]        
        public string Auditor { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        [Required(ErrorMessage = "审核状态不能为空")]
        [MaxLength(50, ErrorMessage = "审核状态不能超过50个字符")]   
        public string AuditStatus { get; set; }

        /// <summary>
        /// 审核备注
        /// </summary>
        [MaxLength(2000, ErrorMessage = "审核状态不能超过2000个字符")]
        public string AuditRemark { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? AuditDate { get; set; }

        /// <summary>
        /// 申请人
        /// </summary>
        [Required(ErrorMessage = "申请人不能为空")]
        [MaxLength(200, ErrorMessage = "申请人不能超过200个字符")]        
        public string Application { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplicationDate { get; set; }

        /// <summary>
        /// 申请人
        /// </summary>
        [MaxLength(200, ErrorMessage = "创建人不能超过200个字符")]
        public string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}
