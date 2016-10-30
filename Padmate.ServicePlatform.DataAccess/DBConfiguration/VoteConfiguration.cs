using Padmate.ServicePlatform.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.DataAccess.DBConfiguration
{
    /// <summary>
    /// 投票
    /// </summary>
    public class VoteConfiguration: EntityTypeConfiguration<Vote>
    {
        internal VoteConfiguration()
        {
            this.HasKey(c => c.Id);

        }
    }
}
