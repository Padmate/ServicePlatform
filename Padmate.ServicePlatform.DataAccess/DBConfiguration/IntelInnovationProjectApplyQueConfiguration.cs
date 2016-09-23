using Padmate.ServicePlatform.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.DataAccess.DBConfiguration
{
    class IntelInnovationProjectApplyQueConfiguration:EntityTypeConfiguration<IntelInnovationProjectApplyQue>
    {
        internal IntelInnovationProjectApplyQueConfiguration()
        {
            this.HasKey(m => m.Id); 
            this.Property(p => p.Auditor).HasMaxLength(200);
            this.Property(p => p.AuditRemark).HasMaxLength(2000);
            this.Property(p => p.Application).HasMaxLength(200);
            this.Property(p => p.AuditStatus).HasMaxLength(50);

            this.HasRequired<IntelInnovationProjectApply>(s => s.IntelInnovationProjectApply)
                    .WithMany(s => s.Ques)
                    .HasForeignKey(s => s.IntelInnovationProjectApplyId);

        }
        
    }
}
