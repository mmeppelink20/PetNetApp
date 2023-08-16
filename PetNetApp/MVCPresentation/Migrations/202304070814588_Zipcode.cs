namespace MVCPresentation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Zipcode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Zipcode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Zipcode");
        }
    }
}
