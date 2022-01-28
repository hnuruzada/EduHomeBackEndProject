using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduHomeBackEndProject.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:50)]
        public string Name { get; set; }
        public List<Course> Courses { get; set; }
    }
}
