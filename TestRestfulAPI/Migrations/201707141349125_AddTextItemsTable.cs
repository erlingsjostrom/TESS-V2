namespace TestRestfulAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTextItemsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TextItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Data = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Content_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contents", t => t.Content_Id)
                .Index(t => t.Content_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TextItems", "Content_Id", "dbo.Contents");
            DropIndex("dbo.TextItems", new[] { "Content_Id" });
            DropTable("dbo.TextItems");
        }
    }
}
