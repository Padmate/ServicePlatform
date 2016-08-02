using Padmate.ServicePlatform.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.DataAccess.DBConfiguration
{
    public class ProjectConfiguration : EntityTypeConfiguration<Project>
    {
        internal ProjectConfiguration()
        {
            this.HasKey(m => m.Id);
            this.Property(m=>m.Creator).HasMaxLength(50);
            this.Property(m => m.Modifier).HasMaxLength(50) ;
            this.Property(m => m.Description).HasMaxLength(2000);
            this.Property(m => m.Type).HasMaxLength(50);

        }
    }
}
