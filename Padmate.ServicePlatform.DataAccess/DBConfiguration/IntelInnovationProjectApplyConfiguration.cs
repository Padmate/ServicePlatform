using Padmate.ServicePlatform.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.DataAccess.DBConfiguration
{
    public class IntelInnovationProjectApplyConfiguration:EntityTypeConfiguration<IntelInnovationProjectApply>
    {
        internal IntelInnovationProjectApplyConfiguration()
        {
            this.HasKey(m => m.Id);
            this.Property(m => m.Description).HasMaxLength(1000);
            this.Property(m => m.Name).HasMaxLength(1000);
            this.Property(m => m.OrganizationName).HasMaxLength(1000);
            this.Property(m => m.FieldScopeCode).HasMaxLength(50);
            this.Property(m => m.FieldScopeName).HasMaxLength(100);
            this.Property(m => m.ProjectStage).HasMaxLength(50);
            this.Property(m => m.BusinessLicense).HasMaxLength(500);
            this.Property(m => m.BusinessAddress).HasMaxLength(2000);
            this.Property(m => m.Website).HasMaxLength(2000);
            this.Property(m => m.WebChatNumber).HasMaxLength(100);
            this.Property(m => m.Principal).HasMaxLength(100);
            this.Property(m => m.PrincipalPosition).HasMaxLength(100);
            this.Property(m => m.PrincipalPhone).HasMaxLength(100);
            this.Property(m => m.PrincipalMail).HasMaxLength(100);
            this.Property(m => m.Contact).HasMaxLength(100);
            this.Property(m => m.ContactPosition).HasMaxLength(100);
            this.Property(m => m.ContactPhone).HasMaxLength(100);
            this.Property(m => m.ContactMail).HasMaxLength(100);
            this.Property(m => m.OrganizationDescription).HasMaxLength(1000);
            this.Property(m => m.CoreTechnology).HasMaxLength(1000);
            this.Property(m => m.Keyword).HasMaxLength(100);


            //与用户一对多
            this.HasRequired<ApplicationUser>(s => s.User)
                    .WithMany(s => s.IntelInnovationProjectApplies)
                    .HasForeignKey(s => s.UserId);

        }
    }
}
