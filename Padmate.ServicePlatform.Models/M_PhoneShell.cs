using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.Models
{
    /// <summary>
    /// 智能手机皮套模型对象
    /// </summary>
    [DataContract]
    public class M_PhoneShell:BaseModel
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public bool IsActivate{get;set;}

        [DataMember]
        public DateTime? ActivateDate { get; set; }

        [DataMember]
        public string PhoneId { get; set; }
    }
}
