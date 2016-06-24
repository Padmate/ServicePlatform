using Padmate.ServicePlatform.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Padmate.ServicePlatform.DataAccess
{
    public class MailAttachmentConfiguration:EntityTypeConfiguration<MailAttachment>
    {
        internal MailAttachmentConfiguration()
        {
            this.HasKey(m => m.Id);
            this.HasRequired<Mail>(s => s.Mail)
                    .WithMany(s => s.MailAttachments)
                    .HasForeignKey(s => s.MailId);
        }
        
    }
}