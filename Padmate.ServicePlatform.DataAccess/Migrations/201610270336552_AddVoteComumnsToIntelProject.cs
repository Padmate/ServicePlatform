namespace Padmate.ServicePlatform.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVoteComumnsToIntelProject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IntelInnovationProjectApplies", "VoteNo", c => c.String(maxLength: 2000));
            AddColumn("dbo.IntelInnovationProjectApplies", "TotalVotes", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.IntelInnovationProjectApplies", "TotalVotes");
            DropColumn("dbo.IntelInnovationProjectApplies", "VoteNo");
        }
    }
}
