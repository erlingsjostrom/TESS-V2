namespace TestRestfulAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateContentTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contents", "EntityType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contents", "EntityType");
        }
    }
}
