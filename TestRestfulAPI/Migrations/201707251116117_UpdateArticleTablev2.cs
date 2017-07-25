namespace TestRestfulAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateArticleTablev2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Articles", new[] { "ArticleNumber" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Articles", "ArticleNumber", unique: true);
        }
    }
}
