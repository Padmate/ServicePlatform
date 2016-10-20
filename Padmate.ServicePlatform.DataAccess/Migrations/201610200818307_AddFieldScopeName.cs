namespace Padmate.ServicePlatform.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldScopeName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IntelInnovationProjectApplies", "FieldScopeCode", c => c.String(maxLength: 50));
            AddColumn("dbo.IntelInnovationProjectApplies", "FieldScopeName", c => c.String(maxLength: 100));
            DropColumn("dbo.IntelInnovationProjectApplies", "FieldScope");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IntelInnovationProjectApplies", "FieldScope", c => c.String(maxLength: 50));
            DropColumn("dbo.IntelInnovationProjectApplies", "FieldScopeName");
            DropColumn("dbo.IntelInnovationProjectApplies", "FieldScopeCode");
        }
    }
}
