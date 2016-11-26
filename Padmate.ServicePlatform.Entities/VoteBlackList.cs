using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.Entities
{
    public class VoteBlackList
    {
        public int Id { get; set; }
        public string VoteNo { get; set; }

        public string ClientIP { get; set; }
    }
}
