namespace tasky.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingToSprintModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sprint", "Release_id", "dbo.Release");
            DropIndex("dbo.Sprint", new[] { "Release_id" });
            AddColumn("dbo.Sprint", "releaseId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Sprint", "releaseId", "dbo.Release", "id", cascadeDelete: true);
            CreateIndex("dbo.Sprint", "releaseId");
            DropColumn("dbo.Sprint", "Release_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sprint", "Release_id", c => c.Int());
            DropIndex("dbo.Sprint", new[] { "releaseId" });
            DropForeignKey("dbo.Sprint", "releaseId", "dbo.Release");
            DropColumn("dbo.Sprint", "releaseId");
            CreateIndex("dbo.Sprint", "Release_id");
            AddForeignKey("dbo.Sprint", "Release_id", "dbo.Release", "id");
        }
    }
}
