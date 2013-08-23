namespace tasky.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class another : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Release", "startDate");
            DropColumn("dbo.Release", "endDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Release", "endDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Release", "startDate", c => c.DateTime(nullable: false));
        }
    }
}
