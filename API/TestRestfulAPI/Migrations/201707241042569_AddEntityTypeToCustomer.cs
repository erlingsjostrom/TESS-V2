namespace TestRestfulAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEntityTypeToCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "EntityType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "EntityType");
        }
    }
}
