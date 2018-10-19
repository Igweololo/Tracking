namespace Tracking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCodeId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tracks", "Code_CodeId", "dbo.Codes");
            DropForeignKey("dbo.Codes", "User_UserId", "dbo.Users");
            DropIndex("dbo.Codes", new[] { "User_UserId" });
            DropIndex("dbo.Tracks", new[] { "Code_CodeId" });
            RenameColumn(table: "dbo.Tracks", name: "Code_CodeId", newName: "CodeId");
            RenameColumn(table: "dbo.Codes", name: "User_UserId", newName: "UserId");
            AlterColumn("dbo.Codes", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Tracks", "CodeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Codes", "UserId");
            CreateIndex("dbo.Tracks", "CodeId");
            AddForeignKey("dbo.Tracks", "CodeId", "dbo.Codes", "CodeId", cascadeDelete: true);
            AddForeignKey("dbo.Codes", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Codes", "UserId", "dbo.Users");
            DropForeignKey("dbo.Tracks", "CodeId", "dbo.Codes");
            DropIndex("dbo.Tracks", new[] { "CodeId" });
            DropIndex("dbo.Codes", new[] { "UserId" });
            AlterColumn("dbo.Tracks", "CodeId", c => c.Int());
            AlterColumn("dbo.Codes", "UserId", c => c.Int());
            RenameColumn(table: "dbo.Codes", name: "UserId", newName: "User_UserId");
            RenameColumn(table: "dbo.Tracks", name: "CodeId", newName: "Code_CodeId");
            CreateIndex("dbo.Tracks", "Code_CodeId");
            CreateIndex("dbo.Codes", "User_UserId");
            AddForeignKey("dbo.Codes", "User_UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Tracks", "Code_CodeId", "dbo.Codes", "CodeId");
        }
    }
}
