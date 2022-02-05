using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduHomeBackEndProject.Models
{
    public class Event
    {
        public int Id { get; set; }
        
        
        public string Image { get; set; }
        [Required]
        
        public DateTime Date { get; set; }
        [Required]
        [StringLength(maximumLength: 150)]
        public string Title { get; set; }
        [Required]
        [StringLength(maximumLength: 1000)]
        public string Description { get; set; }
        [Required]
      

        public DateTime StartTime { get; set; }
        [Required]
       

        public DateTime FinishTime { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string Venue { get; set; }
        public List<EventSpeaker> EventSpeakers { get; set; }
        public List<Comment> Comments { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [NotMapped]
        public List<int> SpeakerIds { get; set; }
    }
}
