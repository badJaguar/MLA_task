namespace MLA_task.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DemoCommonInfoDbModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommonInfo = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DemoTable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 35),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(),
                        DemoCommonInfoModelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DemoCommonInfoDbModels", t => t.DemoCommonInfoModelId, cascadeDelete: true)
                .Index(t => t.DemoCommonInfoModelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DemoTable", "DemoCommonInfoModelId", "dbo.DemoCommonInfoDbModels");
            DropIndex("dbo.DemoTable", new[] { "DemoCommonInfoModelId" });
            DropTable("dbo.DemoTable");
            DropTable("dbo.DemoCommonInfoDbModels");
        }
    }
}
