namespace Padmate.ServicePlatform.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableMails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mails", "ReadTag", c => c.Boolean(nullable: false));
            AddColumn("dbo.Mails", "ReadDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mails", "ReadDate");
            DropColumn("dbo.Mails", "ReadTag");
        }
    }
}
