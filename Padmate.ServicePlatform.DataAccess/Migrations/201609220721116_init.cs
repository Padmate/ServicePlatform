namespace Padmate.ServicePlatform.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Title = c.String(),
                        SubTitle = c.String(),
                        Description = c.String(),
                        Type = c.String(),
                        IsHref = c.Boolean(nullable: false),
                        Href = c.String(),
                        Content = c.String(storeType: "ntext"),
                        ImageId = c.Int(),
                        Creator = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        Modifier = c.String(),
                        ModifiedDate = c.DateTime(),
                        Pubtime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        Description = c.String(maxLength: 200),
                        Sequence = c.Int(nullable: false),
                        ContactScopeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContactScopes", t => t.ContactScopeId, cascadeDelete: true)
                .Index(t => t.ContactScopeId);
            
            CreateTable(
                "dbo.ContactScopes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Scope = c.String(maxLength: 50),
                        Sequence = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VirtualPath = c.String(),
                        PhysicalPath = c.String(),
                        Name = c.String(),
                        SaveName = c.String(),
                        Extension = c.String(maxLength: 10),
                        Sequence = c.Int(nullable: false),
                        Type = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IntelInnovationProjectApplies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 500),
                        Description = c.String(maxLength: 2000),
                        HasExample = c.Boolean(nullable: false),
                        InnovationPoint = c.String(maxLength: 2000),
                        Contact = c.String(maxLength: 200),
                        ContactPhone = c.String(maxLength: 200),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.IntelInnovationProjectApplyAttachments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VirtualPath = c.String(),
                        PhysicalPath = c.String(),
                        Name = c.String(maxLength: 200),
                        SaveName = c.String(maxLength: 200),
                        Extension = c.String(maxLength: 50),
                        IntelInnovationProjectApplyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IntelInnovationProjectApplies", t => t.IntelInnovationProjectApplyId, cascadeDelete: true)
                .Index(t => t.IntelInnovationProjectApplyId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserType = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.MailAttachments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MailId = c.Int(nullable: false),
                        FileName = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Mails", t => t.MailId, cascadeDelete: true)
                .Index(t => t.MailId);
            
            CreateTable(
                "dbo.Mails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        From = c.String(),
                        To = c.String(),
                        Subject = c.String(maxLength: 2000),
                        Cc = c.String(),
                        Body = c.String(storeType: "ntext"),
                        Creator = c.String(maxLength: 50),
                        CreateDate = c.DateTime(nullable: false),
                        Modifier = c.String(maxLength: 50),
                        ModifiedDate = c.DateTime(),
                        SendDate = c.DateTime(),
                        SendTag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProjectDownloads",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        ProjectId = c.Guid(nullable: false),
                        Description = c.String(maxLength: 2000),
                        VirtualPath = c.String(),
                        PhysicalPath = c.String(),
                        SaveName = c.String(maxLength: 50),
                        Extension = c.String(maxLength: 10),
                        Sequence = c.Int(nullable: false),
                        ImageId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(maxLength: 2000),
                        Content = c.String(storeType: "ntext"),
                        Type = c.String(maxLength: 50),
                        Sequence = c.Int(nullable: false),
                        ImageId = c.Int(),
                        Creator = c.String(maxLength: 50),
                        CreateDate = c.DateTime(nullable: false),
                        Modifier = c.String(maxLength: 50),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ProjectDownloads", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.MailAttachments", "MailId", "dbo.Mails");
            DropForeignKey("dbo.IntelInnovationProjectApplies", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserProfiles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.IntelInnovationProjectApplyAttachments", "IntelInnovationProjectApplyId", "dbo.IntelInnovationProjectApplies");
            DropForeignKey("dbo.Contacts", "ContactScopeId", "dbo.ContactScopes");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ProjectDownloads", new[] { "ProjectId" });
            DropIndex("dbo.MailAttachments", new[] { "MailId" });
            DropIndex("dbo.UserProfiles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.IntelInnovationProjectApplyAttachments", new[] { "IntelInnovationProjectApplyId" });
            DropIndex("dbo.IntelInnovationProjectApplies", new[] { "UserId" });
            DropIndex("dbo.Contacts", new[] { "ContactScopeId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Projects");
            DropTable("dbo.ProjectDownloads");
            DropTable("dbo.Mails");
            DropTable("dbo.MailAttachments");
            DropTable("dbo.UserProfiles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.IntelInnovationProjectApplyAttachments");
            DropTable("dbo.IntelInnovationProjectApplies");
            DropTable("dbo.Images");
            DropTable("dbo.ContactScopes");
            DropTable("dbo.Contacts");
            DropTable("dbo.Articles");
        }
    }
}
