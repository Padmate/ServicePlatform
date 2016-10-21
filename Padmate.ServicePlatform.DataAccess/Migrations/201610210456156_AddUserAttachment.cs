namespace Padmate.ServicePlatform.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserAttachment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserAttachments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VirtualPath = c.String(),
                        PhysicalPath = c.String(),
                        Name = c.String(),
                        SaveName = c.String(),
                        Extension = c.String(),
                        Type = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAttachments", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserAttachments", new[] { "UserId" });
            DropTable("dbo.UserAttachments");
        }
    }
}
