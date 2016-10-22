namespace Padmate.ServicePlatform.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModuleTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Modules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ModuleURLId = c.String(maxLength: 200),
                        Title = c.String(maxLength: 200),
                        SubTitle = c.String(maxLength: 200),
                        Description = c.String(maxLength: 200),
                        Type = c.String(maxLength: 100),
                        IsHref = c.Boolean(nullable: false),
                        Href = c.String(),
                        Content = c.String(storeType: "ntext"),
                        ImageId = c.Int(),
                        Sequence = c.Int(nullable: false),
                        Creator = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        Modifier = c.String(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Modules");
        }
    }
}
