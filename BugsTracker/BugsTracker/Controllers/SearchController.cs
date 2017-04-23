namespace BugsTracker.Controllers
{
    using BugsTracker.Models;   
    using System.Linq;
    using System.Web.Mvc;
    using Data;

    public class SearchController : Controller
    {
        // GET: Search for bugs with state New
        public ActionResult New()
        {
            using (var db = new BugsTrackerDbContext())
            {
                var newBugs = db.Bugs
                .Where(a => a.State == State.New)
                .ToList();
                return View(newBugs);
            }
        }

        // GET: Search for bugs with state Open
        public ActionResult Open()
        {
            var db = new BugsTrackerDbContext();
            var openBugs = db.Bugs
                .Where(a => a.State == State.Open)
                .ToList();
            return View(openBugs);
        }

        // GET: Search for bugs with state Closed
        public ActionResult Closed()
        {
            var db = new BugsTrackerDbContext();
            var closedBugs = db.Bugs
                .Where(a => a.State == State.Closed)
                .ToList();
            return View(closedBugs);
        }

        // GET: Search for bugs with state Fixed
        public ActionResult Fixed()
        {
            var db = new BugsTrackerDbContext();
            var fixedBugs = db.Bugs
                .Where(a => a.State == State.Fixed)
                .ToList();
            return View(fixedBugs);
        }

        // GET: Search for bugs with state Fixed
        public ActionResult MyBugs(string AuthorId)
        {
            var db = new BugsTrackerDbContext();

            AuthorId = db.Users
                        .Where(u => u.UserName == this.User.Identity.Name)
                        .First()
                        .Id;
            
            var myBugs = db.Bugs
                .Where(b => b.AuthorId == AuthorId)
                .ToList();


            return View(myBugs);
        }
    }
}