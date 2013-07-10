namespace tasky.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using tasky.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<tasky.DAL.TaskyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(tasky.DAL.TaskyContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Sprints.AddOrUpdate(i => i.title,
            new Sprint
            {
                title = "Unscheduled",
                startDate = DateTime.Parse("2000-1-1"),
                endDate = DateTime.Parse("2000-1-1"),
                stories = new List<Story>()
            });
        }
    }
}
