namespace TestRestfulAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRelationsBetweenContentArticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Content_Id", c => c.Int());
            CreateIndex("dbo.Articles", "Content_Id");
            AddForeignKey("dbo.Articles", "Content_Id", "dbo.Contents", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "Content_Id", "dbo.Contents");
            DropIndex("dbo.Articles", new[] { "Content_Id" });
            DropColumn("dbo.Articles", "Content_Id");
        }
    }
}
