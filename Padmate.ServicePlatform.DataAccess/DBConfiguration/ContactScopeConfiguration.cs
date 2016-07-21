using Padmate.ServicePlatform.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.DataAccess.DBConfiguration
{
    public class ContactScopeConfiguration :EntityTypeConfiguration<ContactScope>
    {
        internal ContactScopeConfiguration()
        {
            this.HasKey(s => s.Id);
            this.Property(s => s.Scope).HasMaxLength(50);

        }
    }
}
