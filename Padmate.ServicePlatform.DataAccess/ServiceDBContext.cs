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

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectDownload> ProjectDownloads { get; set; }

        public DbSet<UserProfile> UserProfile { get; set; }

        public DbSet<UserAttachment> UserAttachments { get; set; }

        public DbSet<IntelInnovationProjectApply> IntelInnovationProjectApplies { get; set; }
        public DbSet<IntelInnovationProjectApplyQue> IntelInnovationProjectApplyQues { get; set; }

        public DbSet<IntelInnovationProjectApplyAttachment> IntelInnovationProjectApplyAttachments { get; set; }

        public DbSet<Module> Modules { get; set; }

        public DbSet<Vote> Votes { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new AtricleConfiguration());
            modelBuilder.Configurations.Add(new ImageConfiguration());
            modelBuilder.Configurations.Add(new MailConfiguration());
            modelBuilder.Configurations.Add(new MailAttachmentConfiguration());
            modelBuilder.Configurations.Add(new ContactConfiguration());
            modelBuilder.Configurations.Add(new ContactScopeConfiguration());
            modelBuilder.Configurations.Add(new ProjectConfiguration());
            modelBuilder.Configurations.Add(new ProjectDownloadConfiguration());
            modelBuilder.Configurations.Add(new UserProfileConfiguration());
            modelBuilder.Configurations.Add(new UserAttachmentConfiguration());
            modelBuilder.Configurations.Add(new IntelInnovationProjectApplyConfiguration());
            modelBuilder.Configurations.Add(new IntelInnovationProjectApplyQueConfiguration());
            modelBuilder.Configurations.Add(new IntelInnovationProjectApplyAttachmentConfiguration());
            modelBuilder.Configurations.Add(new ModuleConfiguration());
            modelBuilder.Configurations.Add(new VoteConfiguration());

        }

        public static ServiceDbContext Create()
        {
            return new ServiceDbContext();
        }
    }
}
