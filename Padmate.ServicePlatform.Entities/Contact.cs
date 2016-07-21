using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// 多个Email之间用‘;’分开
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 多个PhoneNumber之间用‘;’分开
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 联系人描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public int Sequence { get; set; }

        public int ContactScopeId { get; set; }

        public virtual ContactScope ContactScope { get; set; }


    }
}
