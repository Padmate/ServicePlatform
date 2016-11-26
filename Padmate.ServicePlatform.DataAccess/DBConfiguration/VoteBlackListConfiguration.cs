using Padmate.ServicePlatform.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.DataAccess.DBConfiguration
{
    public class VoteBlackListConfiguration: EntityTypeConfiguration<VoteBlackList>
    {
        internal VoteBlackListConfiguration()
        {
            this.HasKey(c => c.Id);

        }
    }
}
