using System;
using System.ComponentModel.DataAnnotations;

namespace EduHomeBackEndProject.Models
{
    public class CourseFeauture
    {
        public int Id { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        [StringLength(maximumLength:25)]
        public string Duration { get; set; }
        [Required]
        [StringLength(maximumLength:25)]
        public string ClassDuration { get; set; }
        [Required]
        [StringLength(maximumLength: 25)]

        public string SkillLevel { get; set; }
        [Required]
        [StringLength(maximumLength: 25)]
        public string Language { get; set; }
        [Required]
        [StringLength(maximumLength: 25)]
        public string Assesments { get; set; }
        [Required]
        [StringLength(maximumLength: 25)]
        public string Price { get; set; }
        [Required]
     
        public int StudentCount { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }

    }
}
