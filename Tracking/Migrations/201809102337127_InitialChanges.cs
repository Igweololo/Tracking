namespace Tracking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialChanges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tracks",
                c => new
                    {
                        TrackId = c.Int(nullable: false, identity: true),
                        Destination = c.String(),
                        Date = c.String(),
                        Consignment = c.String(),
                        Code = c.String(),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.TrackId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tracks", "User_UserId", "dbo.Users");
            DropIndex("dbo.Tracks", new[] { "User_UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Tracks");
        }
    }
}
