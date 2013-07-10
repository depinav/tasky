namespace tasky.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedFK : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Task", "Title", c => c.String(nullable: false, maxLength: 4000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Task", "Title", c => c.String(maxLength: 4000));
        }
    }
}
