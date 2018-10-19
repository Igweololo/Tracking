namespace Tracking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tracks", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tracks", "Date", c => c.String(nullable: false));
        }
    }
}
