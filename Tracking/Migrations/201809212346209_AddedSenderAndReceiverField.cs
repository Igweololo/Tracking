namespace Tracking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSenderAndReceiverField : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Codes", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Tracks", "CodeId", "dbo.Codes");
            DropIndex("dbo.Codes", new[] { "User_UserId" });
            DropIndex("dbo.Tracks", new[] { "CodeId" });
            CreateTable(
                "dbo.Details",
                c => new
                    {
                        DetailId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Digit = c.String(nullable: false),
                        Sender = c.String(nullable: false),
                        Receiver = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.DetailId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.User_UserId);
            
            AddColumn("dbo.Tracks", "Code_DetailId", c => c.Int());
            CreateIndex("dbo.Tracks", "Code_DetailId");
            AddForeignKey("dbo.Tracks", "Code_DetailId", "dbo.Details", "DetailId");
            DropTable("dbo.Codes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Codes",
                c => new
                    {
                        CodeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Digit = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.CodeId);
            
            DropForeignKey("dbo.Tracks", "Code_DetailId", "dbo.Details");
            DropForeignKey("dbo.Details", "User_UserId", "dbo.Users");
            DropIndex("dbo.Tracks", new[] { "Code_DetailId" });
            DropIndex("dbo.Details", new[] { "User_UserId" });
            DropColumn("dbo.Tracks", "Code_DetailId");
            DropTable("dbo.Details");
            CreateIndex("dbo.Tracks", "CodeId");
            CreateIndex("dbo.Codes", "User_UserId");
            AddForeignKey("dbo.Tracks", "CodeId", "dbo.Codes", "CodeId", cascadeDelete: true);
            AddForeignKey("dbo.Codes", "User_UserId", "dbo.Users", "UserId");
        }
    }
}
