using Padmate.ServicePlatform.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.DataAccess.DBConfiguration
{
    public class UserAttachmentConfiguration:EntityTypeConfiguration<UserAttachment>
    {
        internal UserAttachmentConfiguration()
        {
            this.HasKey(m => m.Id);
            this.Property(p => p.Name).HasMaxLength(200);
            this.Property(p => p.SaveName).HasMaxLength(200);
            this.Property(p => p.Extension).HasMaxLength(50);
            this.Property(p => p.Type).HasMaxLength(1);
           

            //与用户一对多
            this.HasRequired<ApplicationUser>(s => s.User)
                    .WithMany(s => s.UserAttachments)
                    .HasForeignKey(s => s.UserId);

        }
    }
}
