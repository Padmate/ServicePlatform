using Padmate.ServicePlatform.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.DataAccess.DBConfiguration
{
    public class ContactConfiguration : EntityTypeConfiguration<Contact>
    {
        internal ContactConfiguration()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.Name).HasMaxLength(50) ;
            this.Property(c => c.Description).HasMaxLength(200);
            this.HasRequired<ContactScope>(s => s.ContactScope)
                    .WithMany(s => s.Contacts)
                    .HasForeignKey(s => s.ContactScopeId);

        }
    }
}
