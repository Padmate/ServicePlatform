namespace Padmate.ServicePlatform.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIntelInnovationProjectApplyQue : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IntelInnovationProjectApplyQues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuditStatus = c.String(maxLength: 50),
                        AuditDate = c.DateTime(),
                        Auditor = c.String(maxLength: 200),
                        AuditRemark = c.String(maxLength: 2000),
                        Application = c.String(maxLength: 200),
                        ApplicationDate = c.DateTime(nullable: false),
                        IntelInnovationProjectApplyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IntelInnovationProjectApplies", t => t.IntelInnovationProjectApplyId, cascadeDelete: true)
                .Index(t => t.IntelInnovationProjectApplyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IntelInnovationProjectApplyQues", "IntelInnovationProjectApplyId", "dbo.IntelInnovationProjectApplies");
            DropIndex("dbo.IntelInnovationProjectApplyQues", new[] { "IntelInnovationProjectApplyId" });
            DropTable("dbo.IntelInnovationProjectApplyQues");
        }
    }
}
