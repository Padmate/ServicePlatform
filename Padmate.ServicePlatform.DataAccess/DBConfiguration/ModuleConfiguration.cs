﻿using Padmate.ServicePlatform.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.DataAccess.DBConfiguration
{
    public class ModuleConfiguration : EntityTypeConfiguration<Module>
    {
        internal ModuleConfiguration()
        {
            this.HasKey(m => m.Id);
            this.Property(c => c.ModuleURLId).HasMaxLength(200);
            this.Property(c => c.Type).HasMaxLength(100);
            this.Property(c => c.Description).HasMaxLength(200);
            this.Property(c => c.Title).HasMaxLength(200);
            this.Property(c => c.SubTitle).HasMaxLength(200);

        }
    }
}
