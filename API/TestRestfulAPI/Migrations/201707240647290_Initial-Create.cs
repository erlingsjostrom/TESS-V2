namespace TestRestfulAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Articles",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            ArticleNumber = c.Int(nullable: false),
            //            Name = c.String(nullable: false),
            //            Description = c.String(nullable: false),
            //            CreatedAt = c.DateTime(nullable: false),
            //            UpdatedAt = c.DateTime(nullable: false),
            //            Content_Id = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Contents", t => t.Content_Id)
            //    .Index(t => t.ArticleNumber, unique: true)
            //    .Index(t => t.Content_Id);
            
            //CreateTable(
            //    "dbo.Contents",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Order = c.Int(nullable: false),
            //            Type = c.String(),
            //            CreatedAt = c.DateTime(nullable: false),
            //            UpdatedAt = c.DateTime(nullable: false),
            //            Offer_Id = c.Int(),
            //            Template_Id = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Offers", t => t.Offer_Id)
            //    .ForeignKey("dbo.Templates", t => t.Template_Id)
            //    .Index(t => t.Offer_Id)
            //    .Index(t => t.Template_Id);
            
            //CreateTable(
            //    "dbo.Offers",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Title = c.String(),
            //            Status = c.String(),
            //            ValidThrough = c.DateTime(nullable: false),
            //            CreatedAt = c.DateTime(nullable: false),
            //            UpdatedAt = c.DateTime(nullable: false),
            //            Customer_Id = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Customers", t => t.Customer_Id)
            //    .Index(t => t.Customer_Id);
            
            //CreateTable(
            //    "dbo.Customers",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(),
            //            CustomerType = c.String(),
            //            WebAddress = c.String(),
            //            CorporateIdentityNumber = c.String(),
            //            CreatedAt = c.DateTime(nullable: false),
            //            UpdatedAt = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Templates",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            EntityType = c.String(),
            //            Name = c.String(nullable: false),
            //            Description = c.String(nullable: false),
            //            CreatedAt = c.DateTime(nullable: false),
            //            UpdatedAt = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.TextItems",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Data = c.String(),
            //            CreatedAt = c.DateTime(nullable: false),
            //            UpdatedAt = c.DateTime(nullable: false),
            //            Content_Id = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Contents", t => t.Content_Id)
            //    .Index(t => t.Content_Id);
            
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.TextItems", "Content_Id", "dbo.Contents");
            //DropForeignKey("dbo.Contents", "Template_Id", "dbo.Templates");
            //DropForeignKey("dbo.Offers", "Customer_Id", "dbo.Customers");
            //DropForeignKey("dbo.Contents", "Offer_Id", "dbo.Offers");
            //DropForeignKey("dbo.Articles", "Content_Id", "dbo.Contents");
            //DropIndex("dbo.TextItems", new[] { "Content_Id" });
            //DropIndex("dbo.Offers", new[] { "Customer_Id" });
            //DropIndex("dbo.Contents", new[] { "Template_Id" });
            //DropIndex("dbo.Contents", new[] { "Offer_Id" });
            //DropIndex("dbo.Articles", new[] { "Content_Id" });
            //DropIndex("dbo.Articles", new[] { "ArticleNumber" });
            //DropTable("dbo.TextItems");
            //DropTable("dbo.Templates");
            //DropTable("dbo.Customers");
            //DropTable("dbo.Offers");
            //DropTable("dbo.Contents");
            //DropTable("dbo.Articles");
        }
    }
}
