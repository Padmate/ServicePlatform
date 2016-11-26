namespace Padmate.ServicePlatform.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatevotebalcklist : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VoteBlackLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VoteNo = c.String(),
                        ClientIP = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VoteBlackLists");
        }
    }
}
