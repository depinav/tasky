namespace tasky.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class storyHasTasksMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Task", "Story_ID", c => c.Int());
            AddForeignKey("dbo.Task", "Story_ID", "dbo.Story", "ID");
            CreateIndex("dbo.Task", "Story_ID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Task", new[] { "Story_ID" });
            DropForeignKey("dbo.Task", "Story_ID", "dbo.Story");
            DropColumn("dbo.Task", "Story_ID");
        }
    }
}
