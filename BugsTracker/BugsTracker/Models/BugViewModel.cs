namespace BugsTracker.Models
{
    using Data;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class BugViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public State State { get; set; }

        //public DateTime DateModified { get; set; }           

        public string AuthorId { get; set; }
    }
}