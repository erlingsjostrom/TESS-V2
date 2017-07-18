namespace TestRestfulAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTemplatesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Templates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntityType = c.String(),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Contents", "Template_Id", c => c.Int());
            CreateIndex("dbo.Contents", "Template_Id");
            AddForeignKey("dbo.Contents", "Template_Id", "dbo.Templates", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contents", "Template_Id", "dbo.Templates");
            DropIndex("dbo.Contents", new[] { "Template_Id" });
            DropColumn("dbo.Contents", "Template_Id");
            DropTable("dbo.Templates");
        }
    }
}
