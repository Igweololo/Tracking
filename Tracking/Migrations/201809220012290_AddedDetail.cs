namespace Tracking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDetail : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tracks", "Code_DetailId", "dbo.Details");
            DropIndex("dbo.Tracks", new[] { "Code_DetailId" });
            RenameColumn(table: "dbo.Tracks", name: "Code_DetailId", newName: "DetailId");
            AlterColumn("dbo.Tracks", "DetailId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tracks", "DetailId");
            AddForeignKey("dbo.Tracks", "DetailId", "dbo.Details", "DetailId", cascadeDelete: true);
            DropColumn("dbo.Tracks", "CodeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tracks", "CodeId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Tracks", "DetailId", "dbo.Details");
            DropIndex("dbo.Tracks", new[] { "DetailId" });
            AlterColumn("dbo.Tracks", "DetailId", c => c.Int());
            RenameColumn(table: "dbo.Tracks", name: "DetailId", newName: "Code_DetailId");
            CreateIndex("dbo.Tracks", "Code_DetailId");
            AddForeignKey("dbo.Tracks", "Code_DetailId", "dbo.Details", "DetailId");
        }
    }
}
