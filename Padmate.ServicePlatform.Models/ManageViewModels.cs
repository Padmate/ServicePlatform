using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Padmate.ServicePlatform.Models
{

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required(ErrorMessage = "请输入密码")]
        [StringLength(15, ErrorMessage = "密码必须至少包含8个字符。", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,15}$", ErrorMessage = "密码必须为大小写字母和数字的组合，不能使用特殊字符，长度在8-15之间")]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认新密码")]
        [Compare("NewPassword", ErrorMessage = "新密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel:BaseModel
    {
        [Required(ErrorMessage="请输入当前密码")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "请输入新密码")]
        [StringLength(15, ErrorMessage = "密码必须至少包含8个字符。", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,15}$", ErrorMessage = "密码必须为大小写字母和数字的组合，不能使用特殊字符，长度在8-15之间")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "请再次输入新密码")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "电话号码")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "代码")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "电话号码")]
        public string PhoneNumber { get; set; }
    }

}