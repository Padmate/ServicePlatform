namespace Padmate.ServicePlatform.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateVoteNoComumnsToIntelProjece : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.IntelInnovationProjectApplies", "VoteNo", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.IntelInnovationProjectApplies", "VoteNo", c => c.Int(nullable: false));
        }
    }
}
