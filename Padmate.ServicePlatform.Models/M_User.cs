﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.Models
{
    public class M_User:BaseModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "用户名不能为空")]
        public string UserName { get; set; }

        //[Required(ErrorMessage = "密码不能为空")]
        //[MinLength(6,ErrorMessage="密码长度不能少于6位")]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "邮箱不能为空")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "邮箱格式不正确")]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string UserType { get; set; }

        public IList<M_Role> Roles { get; set; }

        public IList<M_UserAttachment> UserAttachments { get; set; }

        /// <summary>
        /// 用户营业执照
        /// </summary>
        public IList<M_UserAttachment> UserBusinessLicenseAttachments { get; set; }
    }

    public class SetUserInfoModel:BaseModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "邮箱不能为空")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "邮箱格式不正确")]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

    }
}
