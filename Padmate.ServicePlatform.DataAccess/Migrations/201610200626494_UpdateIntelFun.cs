namespace Padmate.ServicePlatform.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateIntelFun : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.IntelInnovationProjectApplies", "FoundedTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.IntelInnovationProjectApplies", "FoundedTime", c => c.DateTime(nullable: false));
        }
    }
}
