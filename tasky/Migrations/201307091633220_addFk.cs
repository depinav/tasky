namespace tasky.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Task", "TeamMemberId_id", c => c.Int());
            AddForeignKey("dbo.Task", "TeamMemberId_id", "dbo.TeamMember", "id");
            CreateIndex("dbo.Task", "TeamMemberId_id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Task", new[] { "TeamMemberId_id" });
            DropForeignKey("dbo.Task", "TeamMemberId_id", "dbo.TeamMember");
            DropColumn("dbo.Task", "TeamMemberId_id");
        }
    }
}
