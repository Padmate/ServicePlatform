using Padmate.ServicePlatform.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.DataAccess.DBConfiguration
{
    class UserProfileConfiguration:EntityTypeConfiguration<UserProfile>
    {
        internal UserProfileConfiguration()
        {
            this.HasKey(m => m.UserId);
            this.HasRequired<ApplicationUser>(s => s.User)
                    .WithOptional(s => s.UserProfile);
            

        }
    }
}
