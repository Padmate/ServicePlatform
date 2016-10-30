namespace Padmate.ServicePlatform.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateVoteComumnsToIntelProjece : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.IntelInnovationProjectApplies", "VoteNo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.IntelInnovationProjectApplies", "VoteNo", c => c.String(maxLength: 2000));
        }
    }
}
