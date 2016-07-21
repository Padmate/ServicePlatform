using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Padmate.ServicePlatform.DataAccess.DBConfiguration;
using Padmate.ServicePlatform.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Padmate.ServicePlatform.DataAccess
{
    // 可以通过向 ApplicationUser 类添加更多属性来为用户添加配置文件数据。若要了解详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=317594。
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在此处添加自定义用户声明
            return userIdentity;
        }
    }

    public class ServiceDbContext:IdentityDbContext<ApplicationUser>
    {
        public ServiceDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Article> Atricles { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Mail> Mails { get; set; }
        public DbSet<MailAttachment> MailAttachments { get; set; }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactScope> ContactScopes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new AtricleConfiguration());
            modelBuilder.Configurations.Add(new ImageConfiguration());
            modelBuilder.Configurations.Add(new MailConfiguration());
            modelBuilder.Configurations.Add(new MailAttachmentConfiguration());
            modelBuilder.Configurations.Add(new ContactConfiguration());
            modelBuilder.Configurations.Add(new ContactScopeConfiguration());


        }

        public static ServiceDbContext Create()
        {
            return new ServiceDbContext();
        }
    }
}
