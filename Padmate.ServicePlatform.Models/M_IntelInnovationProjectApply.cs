﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.Models
{
    public class M_IntelInnovationProjectApply:BaseModel
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [Required(ErrorMessage = "项目名称不能为空")]
        public string Name { get; set; }

        /// <summary>
        /// 项目简介/描述
        /// </summary>
        [MaxLength(2000, ErrorMessage = "项目简介不能超过2000个字符")]
        public string Description { get; set; }

        /// <summary>
        /// 是否有样机
        /// </summary>
        public bool HasExample { get; set; }

        //// <summary>
        /// 创新点
        /// </summary>
        [MaxLength(2000, ErrorMessage = "创新点内容不能超过2000个字符")]
        public string InnovationPoint { get; set; }
        
        /// 联系人
        /// </summary>
        [MaxLength(200, ErrorMessage = "联系人不能超过200个字符")]
        public string Contact { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [MaxLength(200, ErrorMessage = "联系电话不能超过200个字符")]
        public string ContactPhone { get; set; }

        /// <summary>
        /// 附件列表
        /// </summary>
        public List<M_IntelInnovationProjectApplyAttachment> Attachments { get; set; }
    }
}
