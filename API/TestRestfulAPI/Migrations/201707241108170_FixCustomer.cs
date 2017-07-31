namespace TestRestfulAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Type", c => c.String());
            DropColumn("dbo.Customers", "CustomerType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "CustomerType", c => c.String());
            DropColumn("dbo.Customers", "Type");
        }
    }
}
