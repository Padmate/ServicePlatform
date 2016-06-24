using Padmate.ServicePlatform.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Padmate.ServicePlatform.DataAccess
{
    public class ImageConfiguration : EntityTypeConfiguration<Image>
    {
        internal ImageConfiguration()
        {
            this.HasKey(m => m.Id);

        }
    }
}