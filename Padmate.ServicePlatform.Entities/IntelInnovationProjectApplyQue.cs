using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.Entities
{
    public class IntelInnovationProjectApplyQue
    {
        public int Id { get; set; }

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

        public int IntelInnovationProjectApplyId { get; set; }

        public virtual IntelInnovationProjectApply IntelInnovationProjectApply { get; set; }
       
    }
}
