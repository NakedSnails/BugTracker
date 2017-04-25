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

    public class CommentController : Controller
    {
        // GET: Comment
        [Authorize]
        [HttpGet]
        public ActionResult AddComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var db = new BugsTrackerDbContext())
            {
                var bug = db.Bugs
                   .Where(b => b.Id == id)
                   .First();                

                if (bug == null)
                {
                    return HttpNotFound();
                }

                return View();                              
            }
        }

        //POST: Comment
        [Authorize]
        [HttpPost]
        public ActionResult AddComment(Comment comment)
        {
            if (ModelState.IsValid)
            {
                using (var database = new BugsTrackerDbContext())
                {
                    var authorId = database.Users
                        .Where(u => u.UserName == this.User.Identity.Name)
                        .First()
                        .Id;
                                       
                    comment.AuthorId = authorId;
                    comment.DateAdded = DateTime.Now;

                    database.Comments.Add(comment);
                    database.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            return View(comment);
        }

    }
}