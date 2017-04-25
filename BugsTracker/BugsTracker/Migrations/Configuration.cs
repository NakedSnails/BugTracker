namespace BugsTracker.Migrations
{   
    using System.Data.Entity.Migrations;    

    internal sealed class Configuration : DbMigrationsConfiguration<BugsTracker.Models.BugsTrackerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BugsTracker.Models.BugsTrackerDbContext context)
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
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    //one-to-many 
        //    modelBuilder.Entity<Bug>()
        //                .HasMany<Comment>(s => s.Comments)                
        //                .HasForeignKey(s => s.BugId);
        //}
    }
}
