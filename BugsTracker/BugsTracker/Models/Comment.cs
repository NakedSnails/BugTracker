namespace BugsTracker.Models
{
    using System;  

    public class Comment
    {
        public int CommentId { get; set; }
        
        public string Content { get; set; }

        public DateTime DateAdded { get; set; }

        public virtual ApplicationUser Author { get; set; }

    }
}