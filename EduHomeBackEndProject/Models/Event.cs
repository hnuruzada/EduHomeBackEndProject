using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduHomeBackEndProject.Models
{
    public class Event
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:70)]
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
        [StringLength(maximumLength: 10)]

        public string StartTime { get; set; }
        [Required]
        [StringLength(maximumLength: 10)]

        public string FinishTime { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string Venue { get; set; }
        public List<EventSpeaker> EventSpeakers { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
