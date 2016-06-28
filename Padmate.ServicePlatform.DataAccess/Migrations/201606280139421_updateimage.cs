namespace Padmate.ServicePlatform.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateimage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "VirtualPath", c => c.String());
            AddColumn("dbo.Images", "PhysicalPath", c => c.String());
            AddColumn("dbo.Images", "Name", c => c.String());
            AddColumn("dbo.Images", "SaveName", c => c.String());
            AddColumn("dbo.Images", "Extension", c => c.String(maxLength: 10));
            AlterColumn("dbo.Images", "Type", c => c.String(maxLength: 50));
            DropColumn("dbo.Images", "ImageUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "ImageUrl", c => c.String());
            AlterColumn("dbo.Images", "Type", c => c.String());
            DropColumn("dbo.Images", "Extension");
            DropColumn("dbo.Images", "SaveName");
            DropColumn("dbo.Images", "Name");
            DropColumn("dbo.Images", "PhysicalPath");
            DropColumn("dbo.Images", "VirtualPath");
        }
    }
}
