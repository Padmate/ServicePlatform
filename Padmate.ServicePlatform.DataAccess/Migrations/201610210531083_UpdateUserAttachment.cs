namespace Padmate.ServicePlatform.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserAttachment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserAttachments", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserAttachments", new[] { "UserId" });
            AlterColumn("dbo.UserAttachments", "Name", c => c.String(maxLength: 200));
            AlterColumn("dbo.UserAttachments", "SaveName", c => c.String(maxLength: 200));
            AlterColumn("dbo.UserAttachments", "Extension", c => c.String(maxLength: 50));
            AlterColumn("dbo.UserAttachments", "Type", c => c.String(maxLength: 1));
            AlterColumn("dbo.UserAttachments", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.UserAttachments", "UserId");
            AddForeignKey("dbo.UserAttachments", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAttachments", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserAttachments", new[] { "UserId" });
            AlterColumn("dbo.UserAttachments", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.UserAttachments", "Type", c => c.String());
            AlterColumn("dbo.UserAttachments", "Extension", c => c.String());
            AlterColumn("dbo.UserAttachments", "SaveName", c => c.String());
            AlterColumn("dbo.UserAttachments", "Name", c => c.String());
            CreateIndex("dbo.UserAttachments", "UserId");
            AddForeignKey("dbo.UserAttachments", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
