namespace BugsTracker.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;

    public class BugsTrackerDbContext : IdentityDbContext<ApplicationUser>
    {
        public BugsTrackerDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Bug> Bugs { get; set; }
        public virtual IDbSet<Comment> Comments { get; set; }

        public static BugsTrackerDbContext Create()
        {
            return new BugsTrackerDbContext();
        }
    }
}