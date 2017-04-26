namespace BugsTracker.Models
{
    using Data;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Bug
    {
        public Bug()
        {
            this.Comments = new HashSet<Comment>(); 
        }      

        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]        
        public string Description { get; set; }

        [Required]
        public State State { get; set; }

        public DateTime DateAdded { get; set; } 

        //public DateTime DateModified { get; set; }

        public string AttachmentURL { get; set; }

        [ForeignKey("Author")]
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        
    }
}