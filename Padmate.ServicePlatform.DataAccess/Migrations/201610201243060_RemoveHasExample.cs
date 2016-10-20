namespace Padmate.ServicePlatform.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveHasExample : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.IntelInnovationProjectApplies", "HasExample");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IntelInnovationProjectApplies", "HasExample", c => c.Boolean(nullable: false));
        }
    }
}
