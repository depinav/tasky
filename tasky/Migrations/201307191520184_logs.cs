namespace tasky.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class logs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskLog",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        loggedHours = c.Int(nullable: false),
                        logDate = c.DateTime(nullable: false),
                        taskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Task", t => t.taskId, cascadeDelete: true)
                .Index(t => t.taskId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.TaskLog", new[] { "taskId" });
            DropForeignKey("dbo.TaskLog", "taskId", "dbo.Task");
            DropTable("dbo.TaskLog");
        }
    }
}
