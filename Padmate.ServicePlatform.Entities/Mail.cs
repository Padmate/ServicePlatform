﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Padmate.ServicePlatform.Entities
{
    public class Mail
    {
        public int Id { get; set; }

        /// <summary>
        /// 如果有多个，格式为123@qq.com,456@qq.com
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// 如果有多个，格式为123@qq.com,456@qq.com
        /// </summary>
        public string To { get; set; }
        public string Subject { get; set; }

        /// <summary>
        /// 抄送
        /// 如果有多个，格式为123@qq.com,456@qq.com
        /// </summary>
        public string Cc { get; set; }

        /// <summary>
        /// 邮件内容
        /// </summary>
        [Column("Body", TypeName = "ntext")]
        public string Body { get; set; }

        /// <summary>
        /// 邮件创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 邮件发送时间
        /// </summary>
        public DateTime? SendDate { get; set; }

        /// <summary>
        /// 邮件发送标记
        /// </summary>
        public bool SendTag { get; set; }

        public virtual ICollection<MailAttachment> MailAttachments { get; set; }

        public Mail()
        {
            MailAttachments = new List<MailAttachment>();
        }

    }

   

}