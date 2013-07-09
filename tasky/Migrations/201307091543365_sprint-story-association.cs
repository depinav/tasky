namespace tasky.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sprintstoryassociation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Story", "sprint_id", c => c.Int());
            AddForeignKey("dbo.Story", "sprint_id", "dbo.Sprint", "id");
            CreateIndex("dbo.Story", "sprint_id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Story", new[] { "sprint_id" });
            DropForeignKey("dbo.Story", "sprint_id", "dbo.Sprint");
            DropColumn("dbo.Story", "sprint_id");
        }
    }
}
