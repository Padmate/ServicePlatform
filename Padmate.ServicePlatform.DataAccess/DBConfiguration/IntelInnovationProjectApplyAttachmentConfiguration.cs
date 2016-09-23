using Padmate.ServicePlatform.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.DataAccess.DBConfiguration
{
    class IntelInnovationProjectApplyAttachmentConfiguration:EntityTypeConfiguration<IntelInnovationProjectApplyAttachment>
    {
        internal IntelInnovationProjectApplyAttachmentConfiguration()
        {
            this.HasKey(m => m.Id); 
            this.Property(p => p.Name).HasMaxLength(200);
            this.Property(p => p.SaveName).HasMaxLength(200);
            this.Property(p => p.Extension).HasMaxLength(50);
            this.HasRequired<IntelInnovationProjectApply>(s => s.IntelInnovationProjectApply)
                    .WithMany(s => s.Attachments)
                    .HasForeignKey(s => s.IntelInnovationProjectApplyId);

        }
        
    }
}
