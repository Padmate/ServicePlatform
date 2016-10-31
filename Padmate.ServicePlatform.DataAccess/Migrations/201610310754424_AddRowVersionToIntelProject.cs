namespace Padmate.ServicePlatform.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRowVersionToIntelProject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IntelInnovationProjectApplies", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.IntelInnovationProjectApplies", "RowVersion");
        }
    }
}
