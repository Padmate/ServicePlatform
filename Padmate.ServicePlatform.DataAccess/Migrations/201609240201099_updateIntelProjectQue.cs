namespace Padmate.ServicePlatform.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateIntelProjectQue : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IntelInnovationProjectApplyQues", "Creator", c => c.String(maxLength: 200));
            AddColumn("dbo.IntelInnovationProjectApplyQues", "CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.IntelInnovationProjectApplyQues", "CreateDate");
            DropColumn("dbo.IntelInnovationProjectApplyQues", "Creator");
        }
    }
}
