﻿using Padmate.ServicePlatform.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Padmate.ServicePlatform.DataAccess.DBConfiguration
{
    public class ImageConfiguration : EntityTypeConfiguration<Image>
    {
        internal ImageConfiguration()
        {
            this.HasKey(m => m.Id);
            this.Property(m => m.Extension).HasMaxLength(10);
            this.Property(m => m.Type).HasMaxLength(50);
            this.Property(m => m.LinkHref).HasMaxLength(2000);


        }
    }
}