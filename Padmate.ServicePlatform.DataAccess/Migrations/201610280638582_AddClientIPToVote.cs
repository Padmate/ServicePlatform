namespace Padmate.ServicePlatform.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClientIPToVote : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Votes", "ClientIP", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Votes", "ClientIP");
        }
    }
}
