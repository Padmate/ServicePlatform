using Padmate.ServicePlatform.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Padmate.ServicePlatform.DataAccess.DBConfiguration
{
    public class AtricleConfiguration : EntityTypeConfiguration<Article>
    {
        internal AtricleConfiguration()
        {
            this.HasKey(m => m.Id);
            //this.HasOptional<Image>(s => s.Image)
            //        .(s => s.Articles)
            //        .HasForeignKey(s => s.ImageId);

        }
    }
}