using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.Entities
{
    public class ContactScope
    {
        public int Id { get; set; }

        /// <summary>
        /// 联系人职责范围
        /// </summary>
        public string Scope { get; set; }

        /// <summary>
        /// 排列顺序
        /// </summary>
        public int Sequence { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }

        public ContactScope()
        {
            Contacts = new List<Contact>();
        }
    }
}
