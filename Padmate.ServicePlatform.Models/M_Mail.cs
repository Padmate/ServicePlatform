using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Padmate.ServicePlatform.Models
{
    public class M_Mail:BaseModel
    {
        [Required(ErrorMessage="发件人不能为空")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "发件人邮箱格式不正确")]
        public string From { get; set; }

        /// <summary>
        /// 如果有多个，格式为123@qq.com,456@qq.com
        /// </summary>
        [Required(ErrorMessage = "收件人不能为空")]
        [RegularExpression(@"^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\. 
       ([a-zA-Z]{2,5}){1,25})+)*$"
            , ErrorMessage = "收件人邮箱格式不正确，多个邮箱请用';'隔开")]
        public string To { get; set; }

        [Required(ErrorMessage = "邮件标题不能为空")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "邮件内容不能为空")]
        public string Body { get; set; }

    }

    /// <summary>
    /// 邮件服务器
    /// </summary>
    public class MailSmtp
    {
        public string Server { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
    }

    public class Mail
    {
        /// <summary>
        /// 收件人
        /// 如果有多个，格式为123@qq.com,456@qq.com
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// 抄送
        /// 如果有多个，格式为123@qq.com,456@qq.com
        /// </summary>
        public string Cc { get; set; }
    }
}