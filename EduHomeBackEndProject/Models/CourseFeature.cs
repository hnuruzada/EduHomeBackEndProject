using System.ComponentModel.DataAnnotations;

namespace EduHomeBackEndProject.Models
{
    public class CourseFeature
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 50)]
        public string Name { get; set; }
        [StringLength(maximumLength: 20)]
        public string Value { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
