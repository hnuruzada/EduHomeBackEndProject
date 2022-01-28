using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduHomeBackEndProject.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:70)]
        public string CourseName { get; set; }
        [Required]
        [StringLength(maximumLength:70)]
        public string CourseImage { get; set; }
        
        [StringLength(maximumLength:70)]
        public string Icon { get; set; }
        [Required]
        [StringLength(maximumLength:70)]
        public string Description { get; set; }
        [Required]
        [StringLength(maximumLength: 500)]
        public string AboutCourseInfo { get; set; }
        [Required]
        [StringLength(maximumLength: 500)]
        public string HowToApplyInfo { get; set; }
        [Required]
        [StringLength(maximumLength: 70)]
        public string CertificationInfo { get; set; }
        public List<Comment> Comments { get; set; }
        public int CategoryId { get; set; }
        public Category Category{ get; set; }
        public List<CourseTag> CourseTags { get; set; }
        public CourseFeauture CourseFeauture { get; set; }

    }
}
