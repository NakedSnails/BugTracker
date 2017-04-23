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
    using Data;

    public class BugController : Controller
    {
        // GET: Bug
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        //List Bugs
        public ActionResult List(int page = 1)
        {
            using (var database = new BugsTrackerDbContext())
            {
                var pageSize = 6;

                var bugs = database.Bugs
                    .OrderByDescending(b => b.DateAdded)
                    .Skip((page - 1)* pageSize)
                    .Include(a => a.Author)
                    .ToList();

                ViewBag.CurrentPage = page;

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
        [Authorize]
        [HttpGet]        
        public ActionResult Report()
        {
            return View();
        }

        //POST: Bug/Report
        [Authorize]
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

        //GET: Bug/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new BugsTrackerDbContext())
            {
                var bug = database.Bugs
                    .Where(b => b.Id == id)
                    .First();

                if(bug == null)
                {
                    return HttpNotFound();
                }

                var model = new BugViewModel();
                model.Id = bug.Id;
                model.Title = bug.Title;
                model.Description = bug.Description;
                model.State = bug.State;

                return View(model);
            }
        }

        //POST: Bug/Edit
        [HttpPost]
        public ActionResult Edit(BugViewModel model)
        {
            if(ModelState.IsValid)
            {
                using (var database = new BugsTrackerDbContext())
                {
                    var bug = database.Bugs
                        .FirstOrDefault(b => b.Id == model.Id);

                    bug.Title = model.Title;
                    bug.Description = model.Description;
                    bug.State = model.State;
                    //bug.DateModified = model.DateModified;

                    database.Entry(bug).State = EntityState.Modified;
                    database.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }       

        //GET: Bug/AddComment        
        public ActionResult AddComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new BugsTrackerDbContext())
            {
                var bug = database.Bugs
                    .Where(b => b.Id == id)
                    .First();

                if (bug == null)
                {
                    return HttpNotFound();
                }

                var model = new BugViewModel();
                model.Id = bug.Id;
                model.Title = bug.Title;
                model.Description = bug.Description;
                model.State = bug.State;
                return View(model);
            }
        }

        //POST: Bug/AddComment
        [HttpPost]
        public ActionResult AddComment(BugViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var database = new BugsTrackerDbContext())
                {
                    var bug = database.Bugs
                        .FirstOrDefault(b => b.Id == model.Id);

                    bug.Comments = model.Comments.ToList();
                    bug.Title = model.Title;
                    bug.Description = model.Description;
                    bug.State = model.State;

                    database.Entry(bug).State = EntityState.Modified;
                    database.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        // GET: Search
        public ActionResult Search()
        {
            var db = new BugsTrackerDbContext();
            var newBugs = db.Bugs
                .Where(a => a.State == State.New)
                .ToList();

            return View(newBugs);
        }
    }
}