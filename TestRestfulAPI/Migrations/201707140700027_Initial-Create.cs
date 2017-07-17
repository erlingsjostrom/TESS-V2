namespace TestRestfulAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            
            CreateTable(
                "dbo.Contents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Order = c.Int(nullable: false),
                        Type = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Offer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Offers", t => t.Offer_Id)
                .Index(t => t.Offer_Id);
            
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Offers", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Contents", "Offer_Id", "dbo.Offers");
            DropIndex("dbo.Offers", new[] { "Customer_Id" });
            DropIndex("dbo.Contents", new[] { "Offer_Id" });
            DropIndex("dbo.Articles", new[] { "ArticleNumber" });
            DropTable("dbo.Customers");
            DropTable("dbo.Offers");
            DropTable("dbo.Contents");
            DropTable("dbo.Articles");
        }
    }
}
