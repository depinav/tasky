namespace tasky.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class storytotasks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Task", "storyId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Task", "storyId", "dbo.Story", "ID", cascadeDelete: true);
            CreateIndex("dbo.Task", "storyId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Task", new[] { "storyId" });
            DropForeignKey("dbo.Task", "storyId", "dbo.Story");
            DropColumn("dbo.Task", "storyId");
        }
    }
}
