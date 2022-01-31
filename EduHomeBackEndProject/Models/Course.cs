using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduHomeBackEndProject.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:70)]
        public string CourseName { get; set; }
        
        public string CourseImage { get; set; }
        
        [StringLength(maximumLength:70)]
        public string Icon { get; set; }
        [Required]
        [StringLength(maximumLength:500)]
        public string Description { get; set; }
        [Required]
        [StringLength(maximumLength: 500)]
        public string AboutCourseInfo { get; set; }
        [Required]
        [StringLength(maximumLength: 500)]
        public string HowToApplyInfo { get; set; }
        [Required]
        [StringLength(maximumLength: 500)]
        public string CertificationInfo { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public List<CourseFeature> CourseFeatures { get; set; }
        public List<Comment> Comments { get; set; }
        public int CategoryId { get; set; }
        public Category Category{ get; set; }
        public List<CourseTag> CourseTags { get; set; }
        
        [NotMapped]
        public IFormFile CourseImgFile { get; set; }
        
        [NotMapped]
        public List<int> TagIds { get; set; }

    }
}
