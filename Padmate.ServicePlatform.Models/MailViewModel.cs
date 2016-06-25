using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Padmate.ServicePlatform.Models
{
    public class MailViewModel
    {
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "发件人邮箱格式不正确")]
        public string From { get; set; }

        /// <summary>
        /// 如果有多个，格式为123@qq.com,456@qq.com
        /// </summary>
        [RegularExpression(@"^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\. 
       ([a-zA-Z]{2,5}){1,25})+)*$"
            , ErrorMessage = "收件人邮箱格式不正确，多个邮箱请用';'隔开")]
        public string To { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

    }
}