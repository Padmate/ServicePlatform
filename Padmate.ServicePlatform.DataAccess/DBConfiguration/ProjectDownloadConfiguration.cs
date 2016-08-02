using Padmate.ServicePlatform.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.DataAccess.DBConfiguration
{
    public class ProjectDownloadConfiguration:EntityTypeConfiguration<ProjectDownload>
    {
        internal ProjectDownloadConfiguration()
        {
            this.HasKey(m => m.Id);
            this.HasRequired<Project>(s => s.Project)
                    .WithMany(s => s.ProjectDownloads)
                    .HasForeignKey(s => s.ProjectId);
            this.Property(p => p.Description).HasMaxLength(2000);
            this.Property(p => p.SaveName).HasMaxLength(50);
            this.Property(p => p.Extension).HasMaxLength(10);

        }
    }
}
