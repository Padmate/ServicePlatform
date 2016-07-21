namespace Padmate.ServicePlatform.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateMail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mails", "Creator", c => c.String(maxLength: 50));
            AddColumn("dbo.Mails", "Modifier", c => c.String(maxLength: 50));
            AddColumn("dbo.Mails", "ModifiedDate", c => c.DateTime());
            AlterColumn("dbo.Mails", "Subject", c => c.String(maxLength: 2000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Mails", "Subject", c => c.String());
            DropColumn("dbo.Mails", "ModifiedDate");
            DropColumn("dbo.Mails", "Modifier");
            DropColumn("dbo.Mails", "Creator");
        }
    }
}
