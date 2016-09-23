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
        /// 审核状态:审核中\审核通过\拒绝
        /// </summary>
        public string AuditStatus { get; set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        public string AuditDate { get; set; }

        /// <summary>
        /// 审核备注
        /// </summary>
        public string AuditRemark { get; set; }

        /// <summary>
        /// 创建者
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
        /// 修改时间
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
    }
}
