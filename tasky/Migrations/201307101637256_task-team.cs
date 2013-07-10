namespace tasky.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class taskteam : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Story",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        title = c.String(nullable: false, maxLength: 4000),
                        description = c.String(maxLength: 4000),
                        points = c.Int(nullable: false),
                        status = c.String(nullable: false, maxLength: 4000),
                        sprintId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sprint", t => t.sprintId, cascadeDelete: true)
                .Index(t => t.sprintId);
            
            CreateTable(
                "dbo.Sprint",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        title = c.String(maxLength: 4000),
                        startDate = c.DateTime(nullable: false),
                        endDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.TeamMember",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 4000),
                        email = c.String(maxLength: 4000),
                        hash = c.String(maxLength: 4000),
                        salt = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Task",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 4000),
                        Description = c.String(maxLength: 4000),
                        Estimate_Hours = c.Int(nullable: false),
                        Remaining_Hours = c.Int(nullable: false),
                        Status = c.String(maxLength: 4000),
                        TeamMemberId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TeamMember", t => t.TeamMemberId, cascadeDelete: true)
                .Index(t => t.TeamMemberId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Task", new[] { "TeamMemberId" });
            DropIndex("dbo.Story", new[] { "sprintId" });
            DropForeignKey("dbo.Task", "TeamMemberId", "dbo.TeamMember");
            DropForeignKey("dbo.Story", "sprintId", "dbo.Sprint");
            DropTable("dbo.Task");
            DropTable("dbo.TeamMember");
            DropTable("dbo.Sprint");
            DropTable("dbo.Story");
        }
    }
}
