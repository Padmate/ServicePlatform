namespace Padmate.ServicePlatform.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactorIntelFun : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IntelInnovationProjectApplies", "OrganizationName", c => c.String(maxLength: 1000));
            AddColumn("dbo.IntelInnovationProjectApplies", "FieldScope", c => c.String(maxLength: 50));
            AddColumn("dbo.IntelInnovationProjectApplies", "ProjectStage", c => c.String(maxLength: 50));
            AddColumn("dbo.IntelInnovationProjectApplies", "FoundedTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.IntelInnovationProjectApplies", "BusinessLicense", c => c.String(maxLength: 500));
            AddColumn("dbo.IntelInnovationProjectApplies", "BusinessAddress", c => c.String(maxLength: 2000));
            AddColumn("dbo.IntelInnovationProjectApplies", "Website", c => c.String(maxLength: 2000));
            AddColumn("dbo.IntelInnovationProjectApplies", "WebChatNumber", c => c.String(maxLength: 100));
            AddColumn("dbo.IntelInnovationProjectApplies", "Principal", c => c.String(maxLength: 100));
            AddColumn("dbo.IntelInnovationProjectApplies", "PrincipalPosition", c => c.String(maxLength: 100));
            AddColumn("dbo.IntelInnovationProjectApplies", "PrincipalPhone", c => c.String(maxLength: 100));
            AddColumn("dbo.IntelInnovationProjectApplies", "PrincipalMail", c => c.String(maxLength: 100));
            AddColumn("dbo.IntelInnovationProjectApplies", "ContactPosition", c => c.String(maxLength: 100));
            AddColumn("dbo.IntelInnovationProjectApplies", "ContactMail", c => c.String(maxLength: 100));
            AddColumn("dbo.IntelInnovationProjectApplies", "OrganizationDescription", c => c.String(maxLength: 1000));
            AddColumn("dbo.IntelInnovationProjectApplies", "CoreTechnology", c => c.String(maxLength: 1000));
            AddColumn("dbo.IntelInnovationProjectApplies", "Keyword", c => c.String(maxLength: 100));
            AlterColumn("dbo.IntelInnovationProjectApplies", "Name", c => c.String(maxLength: 1000));
            AlterColumn("dbo.IntelInnovationProjectApplies", "Description", c => c.String(maxLength: 1000));
            AlterColumn("dbo.IntelInnovationProjectApplies", "Contact", c => c.String(maxLength: 100));
            AlterColumn("dbo.IntelInnovationProjectApplies", "ContactPhone", c => c.String(maxLength: 100));
            DropColumn("dbo.IntelInnovationProjectApplies", "InnovationPoint");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IntelInnovationProjectApplies", "InnovationPoint", c => c.String(maxLength: 2000));
            AlterColumn("dbo.IntelInnovationProjectApplies", "ContactPhone", c => c.String(maxLength: 200));
            AlterColumn("dbo.IntelInnovationProjectApplies", "Contact", c => c.String(maxLength: 200));
            AlterColumn("dbo.IntelInnovationProjectApplies", "Description", c => c.String(maxLength: 2000));
            AlterColumn("dbo.IntelInnovationProjectApplies", "Name", c => c.String(maxLength: 500));
            DropColumn("dbo.IntelInnovationProjectApplies", "Keyword");
            DropColumn("dbo.IntelInnovationProjectApplies", "CoreTechnology");
            DropColumn("dbo.IntelInnovationProjectApplies", "OrganizationDescription");
            DropColumn("dbo.IntelInnovationProjectApplies", "ContactMail");
            DropColumn("dbo.IntelInnovationProjectApplies", "ContactPosition");
            DropColumn("dbo.IntelInnovationProjectApplies", "PrincipalMail");
            DropColumn("dbo.IntelInnovationProjectApplies", "PrincipalPhone");
            DropColumn("dbo.IntelInnovationProjectApplies", "PrincipalPosition");
            DropColumn("dbo.IntelInnovationProjectApplies", "Principal");
            DropColumn("dbo.IntelInnovationProjectApplies", "WebChatNumber");
            DropColumn("dbo.IntelInnovationProjectApplies", "Website");
            DropColumn("dbo.IntelInnovationProjectApplies", "BusinessAddress");
            DropColumn("dbo.IntelInnovationProjectApplies", "BusinessLicense");
            DropColumn("dbo.IntelInnovationProjectApplies", "FoundedTime");
            DropColumn("dbo.IntelInnovationProjectApplies", "ProjectStage");
            DropColumn("dbo.IntelInnovationProjectApplies", "FieldScope");
            DropColumn("dbo.IntelInnovationProjectApplies", "OrganizationName");
        }
    }
}
