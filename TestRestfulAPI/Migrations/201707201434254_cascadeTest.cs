namespace TestRestfulAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cascadeTest : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Articles", "Content_Id", "dbo.Contents");
            DropForeignKey("dbo.Contents", "Offer_Id", "dbo.Offers");
            DropForeignKey("dbo.Contents", "Template_Id", "dbo.Templates");
            DropForeignKey("dbo.TextItems", "Content_Id", "dbo.Contents");
            //AddColumn("dbo.Articles", "Content_Id1", c => c.Int());
            //AddColumn("dbo.Contents", "Offer_Id1", c => c.Int());
            //AddColumn("dbo.Contents", "Template_Id1", c => c.Int());
            //AddColumn("dbo.TextItems", "Content_Id1", c => c.Int());
            //CreateIndex("dbo.Articles", "Content_Id1");
            //CreateIndex("dbo.Contents", "Offer_Id1");
            //CreateIndex("dbo.Contents", "Template_Id1");
            //CreateIndex("dbo.TextItems", "Content_Id1");
            //AddForeignKey("dbo.Articles", "Content_Id1", "dbo.Contents", "Id");
            AddForeignKey("dbo.Articles", "Content_Id", "dbo.Contents", "Id", cascadeDelete: true);
            //AddForeignKey("dbo.Contents", "Offer_Id1", "dbo.Offers", "Id");
            //AddForeignKey("dbo.Contents", "Template_Id1", "dbo.Templates", "Id");
            AddForeignKey("dbo.TextItems", "Content_Id", "dbo.Contents", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Contents", "Offer_Id", "dbo.Offers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Contents", "Template_Id", "dbo.Templates", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contents", "Template_Id", "dbo.Templates");
            DropForeignKey("dbo.Contents", "Offer_Id", "dbo.Offers");
            DropForeignKey("dbo.TextItems", "Content_Id", "dbo.Contents");
            //DropForeignKey("dbo.Contents", "Template_Id1", "dbo.Templates");
            //DropForeignKey("dbo.Contents", "Offer_Id1", "dbo.Offers");
            DropForeignKey("dbo.Articles", "Content_Id", "dbo.Contents");
            //DropForeignKey("dbo.Articles", "Content_Id1", "dbo.Contents");
            //DropIndex("dbo.TextItems", new[] { "Content_Id1" });
            //DropIndex("dbo.Contents", new[] { "Template_Id1" });
            //DropIndex("dbo.Contents", new[] { "Offer_Id1" });
            //DropIndex("dbo.Articles", new[] { "Content_Id1" });
            //DropColumn("dbo.TextItems", "Content_Id1");
            //DropColumn("dbo.Contents", "Template_Id1");
            //DropColumn("dbo.Contents", "Offer_Id1");
            //DropColumn("dbo.Articles", "Content_Id1");
            AddForeignKey("dbo.TextItems", "Content_Id", "dbo.Contents", "Id");
            AddForeignKey("dbo.Contents", "Template_Id", "dbo.Templates", "Id");
            AddForeignKey("dbo.Contents", "Offer_Id", "dbo.Offers", "Id");
            AddForeignKey("dbo.Articles", "Content_Id", "dbo.Contents", "Id");
        }
    }
}
