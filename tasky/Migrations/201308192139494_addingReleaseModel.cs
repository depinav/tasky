namespace tasky.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingReleaseModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Release",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        title = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Sprint", "Release_id", c => c.Int());
            AddForeignKey("dbo.Sprint", "Release_id", "dbo.Release", "id");
            CreateIndex("dbo.Sprint", "Release_id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Sprint", new[] { "Release_id" });
            DropForeignKey("dbo.Sprint", "Release_id", "dbo.Release");
            DropColumn("dbo.Sprint", "Release_id");
            DropTable("dbo.Release");
        }
    }
}
