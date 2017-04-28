namespace BugsTracker.Models
{
    using Data;   
    using System.Collections.Generic;   

    public class BugSearchModel
    {    
        public int Id { get; set; }        
       
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public State State { get; set; }

        public RadioButton RadioButton { get; set; }

        public List<string> Comments { get; set; }

        public string AuthorId { get; set; }
    }
}