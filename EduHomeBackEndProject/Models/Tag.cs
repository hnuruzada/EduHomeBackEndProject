using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduHomeBackEndProject.Models
{
    public class Tag
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:30)]
        public string Name { get; set; }
        public List<CourseTag> CourseTags { get; set; }
    }
}
