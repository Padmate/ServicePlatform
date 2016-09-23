using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.Entities
{
    /// <summary>
    /// 可以通过向 ApplicationUser 类添加更多属性来为用户添加配置文件数据。若要了解详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=317594。 
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// 用户类型:个人用户/企业用户
        /// </summary>
        public string UserType { get; set; }

        /// <summary>
        /// 用户资料
        /// </summary>
        public virtual UserProfile UserProfile { get; set; }

        /// <summary>
        /// 英特尔创新创业项目申请
        /// </summary>
        public virtual ICollection<IntelInnovationProjectApply> IntelInnovationProjectApplies { get; set; }

        /// <summary>
        /// 用户资料
        /// </summary>
        //public virtual UserProfile UserProfile { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在此处添加自定义用户声明
            return userIdentity;
        }

        public ApplicationUser() {
            IntelInnovationProjectApplies = new List<IntelInnovationProjectApply>();
        
        }

    }

    public class User
    {
        public int AccessFailedCount { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Id { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string SecurityStamp { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string UserName { get; set; }

        public string UserType { get; set; }
        public ICollection<Role> Roles { get; set; }

        public UserProfile UserProfile { get; set; }

        public ApplicationUser ConverToApplicationUser(User user)
        {
            ApplicationUser result = new ApplicationUser();
            if (user != null)
            {
                result.Id = user.Id;
                result.UserName = user.UserName;
                result.PasswordHash = user.PasswordHash;
                result.Email = user.Email;
                result.EmailConfirmed = user.EmailConfirmed;
                result.PhoneNumber = user.PhoneNumber;
                result.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
                result.SecurityStamp = user.SecurityStamp;
                result.TwoFactorEnabled = user.TwoFactorEnabled;
                result.AccessFailedCount = user.AccessFailedCount;
                result.LockoutEnabled = user.LockoutEnabled;
                result.LockoutEndDateUtc = user.LockoutEndDateUtc;

                
            }
            return result;
        }
    }
}
