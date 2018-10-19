namespace Tracking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCodeTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tracks", "User_UserId", "dbo.Users");
            DropIndex("dbo.Tracks", new[] { "User_UserId" });
            CreateTable(
                "dbo.Codes",
                c => new
                    {
                        CodeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Digit = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.CodeId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.User_UserId);
            
            AddColumn("dbo.Tracks", "Code_CodeId", c => c.Int());
            CreateIndex("dbo.Tracks", "Code_CodeId");
            AddForeignKey("dbo.Tracks", "Code_CodeId", "dbo.Codes", "CodeId");
            DropColumn("dbo.Tracks", "Code");
            DropColumn("dbo.Tracks", "User_UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tracks", "User_UserId", c => c.Int());
            AddColumn("dbo.Tracks", "Code", c => c.String());
            DropForeignKey("dbo.Codes", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Tracks", "Code_CodeId", "dbo.Codes");
            DropIndex("dbo.Tracks", new[] { "Code_CodeId" });
            DropIndex("dbo.Codes", new[] { "User_UserId" });
            DropColumn("dbo.Tracks", "Code_CodeId");
            DropTable("dbo.Codes");
            CreateIndex("dbo.Tracks", "User_UserId");
            AddForeignKey("dbo.Tracks", "User_UserId", "dbo.Users", "UserId");
        }
    }
}
