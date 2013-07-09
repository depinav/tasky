namespace tasky.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class task : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Task",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 4000),
                        Description = c.String(maxLength: 4000),
                        Estimate_Hours = c.Int(nullable: false),
                        Remaining_Hours = c.Int(nullable: false),
                        Status = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Task");
        }
    }
}
