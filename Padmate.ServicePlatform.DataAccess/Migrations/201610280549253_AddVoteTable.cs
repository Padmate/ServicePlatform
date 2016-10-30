namespace Padmate.ServicePlatform.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVoteTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VoteNo = c.String(),
                        VoteTime = c.DateTime(nullable: false),
                        DeviceId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.IntelInnovationProjectApplies", "VoteNo", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.IntelInnovationProjectApplies", "VoteNo", c => c.Int());
            DropTable("dbo.Votes");
        }
    }
}
