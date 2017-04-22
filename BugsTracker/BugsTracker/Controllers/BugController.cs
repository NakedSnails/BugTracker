namespace BugsTracker.Controllers
{
    using BugsTracker.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    public class BugController : Controller
    {
        // GET: Bug
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        //List Bugs
        public ActionResult List()
        {
            using (var database = new BugsTrackerDbContext())
            {
                var bugs = database.Bugs
                    .Include(a => a.Author)
                    .ToList();

                return View(bugs);
            }                
        }

        //GET: Bug/Details
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new BugsTrackerDbContext())
            {
                var bug = database.Bugs
                    .Where(a => a.Id == id)
                    .Include(a => a.Author)
                    .First();

                if(bug == null)
                {
                    return HttpNotFound();
                }                

                return View(bug);
            }
        }

        //GET: Bug/Report
        public ActionResult Report()
        {
            return View();
        }

        //POST: Bug/Report
        [HttpPost]
        public ActionResult Report(Bug bug)
        {
            if(ModelState.IsValid)
            {
                using (var database = new BugsTrackerDbContext())
                {
                    var authorId = database.Users
                        .Where(u => u.UserName == this.User.Identity.Name)
                        .First()
                        .Id;

                    bug.AuthorId = authorId;
                    bug.DateAdded = DateTime.Now;

                    database.Bugs.Add(bug);
                    database.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            return View(bug);
        }


        //Search 
        //public ActionResult Search()
        //{
        //    using (var database = new BugsTrackerDbContext())
        //    {
        //        var bugs = database.Bugs
        //            .Include(a => a.Author)
        //            .ToList();
        //
        //        return View(bugs);
        //    }
        //}
    }
}