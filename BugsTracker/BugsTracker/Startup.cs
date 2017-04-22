using BugsTracker.Migrations;
using BugsTracker.Models;
using Microsoft.Owin;
using Owin;
using System.Data.Entity;
[assembly: OwinStartupAttribute(typeof(BugsTracker.Startup))]

namespace BugsTracker
{ 
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<BugsTrackerDbContext, Configuration>());
            ConfigureAuth(app);
        }
    }
}
