namespace Tracking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Codes", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Codes", "Digit", c => c.String(nullable: false));
            AlterColumn("dbo.Tracks", "Destination", c => c.String(nullable: false));
            AlterColumn("dbo.Tracks", "Date", c => c.String(nullable: false));
            AlterColumn("dbo.Tracks", "Consignment", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tracks", "Consignment", c => c.String());
            AlterColumn("dbo.Tracks", "Date", c => c.String());
            AlterColumn("dbo.Tracks", "Destination", c => c.String());
            AlterColumn("dbo.Codes", "Digit", c => c.String());
            AlterColumn("dbo.Codes", "Name", c => c.String());
        }
    }
}
