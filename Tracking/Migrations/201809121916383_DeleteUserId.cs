namespace Tracking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteUserId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Codes", "UserId", "dbo.Users");
            DropIndex("dbo.Codes", new[] { "UserId" });
            RenameColumn(table: "dbo.Codes", name: "UserId", newName: "User_UserId");
            AlterColumn("dbo.Codes", "User_UserId", c => c.Int());
            CreateIndex("dbo.Codes", "User_UserId");
            AddForeignKey("dbo.Codes", "User_UserId", "dbo.Users", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Codes", "User_UserId", "dbo.Users");
            DropIndex("dbo.Codes", new[] { "User_UserId" });
            AlterColumn("dbo.Codes", "User_UserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Codes", name: "User_UserId", newName: "UserId");
            CreateIndex("dbo.Codes", "UserId");
            AddForeignKey("dbo.Codes", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
    }
}
