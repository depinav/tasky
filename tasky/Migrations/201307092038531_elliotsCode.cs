namespace tasky.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class elliotsCode : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Story", "sprint_id", "dbo.Sprint");
            DropIndex("dbo.Story", new[] { "sprint_id" });
            AddColumn("dbo.Story", "sprintId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Story", "sprintId", "dbo.Sprint", "id", cascadeDelete: true);
            CreateIndex("dbo.Story", "sprintId");
            DropColumn("dbo.Story", "sprint_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Story", "sprint_id", c => c.Int());
            DropIndex("dbo.Story", new[] { "sprintId" });
            DropForeignKey("dbo.Story", "sprintId", "dbo.Sprint");
            DropColumn("dbo.Story", "sprintId");
            CreateIndex("dbo.Story", "sprint_id");
            AddForeignKey("dbo.Story", "sprint_id", "dbo.Sprint", "id");
        }
    }
}
