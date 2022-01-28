using System;
using System.ComponentModel.DataAnnotations;

namespace EduHomeBackEndProject.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:200)]
        public string Message { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }

    }
}
