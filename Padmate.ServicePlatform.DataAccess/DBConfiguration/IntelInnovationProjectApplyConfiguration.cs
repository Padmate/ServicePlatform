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
            this.Property(m => m.Description).HasMaxLength(2000);
            this.Property(m => m.Name).HasMaxLength(500);
            this.Property(m => m.InnovationPoint).HasMaxLength(2000);
            this.Property(m => m.Contact).HasMaxLength(200);
            this.Property(m => m.ContactPhone).HasMaxLength(200);

            //与用户一对多
            this.HasRequired<ApplicationUser>(s => s.User)
                    .WithMany(s => s.IntelInnovationProjectApplies)
                    .HasForeignKey(s => s.UserId);

        }
    }
}
