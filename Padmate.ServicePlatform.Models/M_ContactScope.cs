using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.Models
{
    public class M_ContactScope:BaseModel
    {
        public string Id { get; set; }


        [Required(ErrorMessage = "负责范围不能为空")]
        [MaxLength(50,ErrorMessage="负责范围不能超过50个字符")]
        /// <summary>
        /// 职责范围
        /// </summary>
        public string Scope { get; set; }


        [RegularExpression(@"^[1-9]\d*|0$", ErrorMessage = "顺序只能是0或正整数")]
        /// <summary>
        /// 联系人类型
        /// </summary>
        public string Sequence { get; set; }
    }
}
