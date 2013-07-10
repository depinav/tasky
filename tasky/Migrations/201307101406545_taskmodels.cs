namespace tasky.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class taskmodels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Story", "sprintId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Story", "sprintId", "dbo.Sprint", "id", cascadeDelete: true);
            CreateIndex("dbo.Story", "sprintId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Story", new[] { "sprintId" });
            DropForeignKey("dbo.Story", "sprintId", "dbo.Sprint");
            DropColumn("dbo.Story", "sprintId");
        }
    }
}
