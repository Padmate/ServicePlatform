using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.Models
{
    public class M_Contact:BaseModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "名称不能为空")]
        [MaxLength(50, ErrorMessage = "名称不能超过50个字符")]
        public string Name { get; set; }

        /// <summary>
        /// 多个Email之间用‘;’分开
        /// </summary>
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "邮箱格式不正确")]
        public string Email { get; set; }

        /// <summary>
        /// 多个PhoneNumber之间用‘;’分开
        /// </summary>
        public string PhoneNumber { get; set; }


        /// <summary>
        /// 联系人描述
        /// </summary>
        [MaxLength(200, ErrorMessage = "联系人描述不能超过200个字符")]        
        public string Description { get; set; }


        /// <summary>
        /// 顺序
        /// </summary>
        [RegularExpression(@"^[1-9]\d*|0$", ErrorMessage = "顺序只能是0或正整数")]
        public string Sequence { get; set; }

        [Required(ErrorMessage = "职责范围不能为空")]
        public string ContactScopeId { get; set; }

        public string Scope { get; set; }
    }
}
