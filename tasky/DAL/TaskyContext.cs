using System;
using System.Collections.Generic;
using System.Data.Entity;
using tasky.Models;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Migrations;

namespace tasky.DAL
{
    public class TaskyContext : DbContext
    {
        public DbSet<Story> Stories { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskLog> TaskLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DbContext, Configuration>());
        }
    }

    public class Configuration : DbMigrationsConfiguration<DbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
    }
}