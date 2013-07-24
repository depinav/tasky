using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using tasky.Models;

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