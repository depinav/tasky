namespace tasky.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Task", "TeamMemberId_id", "dbo.TeamMember");
            DropIndex("dbo.Task", new[] { "TeamMemberId_id" });
            AddColumn("dbo.Task", "TeamMember", c => c.String(maxLength: 4000));
            DropColumn("dbo.Task", "TeamMemberId_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Task", "TeamMemberId_id", c => c.Int());
            DropColumn("dbo.Task", "TeamMember");
            CreateIndex("dbo.Task", "TeamMemberId_id");
            AddForeignKey("dbo.Task", "TeamMemberId_id", "dbo.TeamMember", "id");
        }
    }
}
