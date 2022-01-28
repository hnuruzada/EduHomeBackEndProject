using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduHomeBackEndProject.Models
{
    public class Faculty
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:70)]
        public string FacultyName { get; set; }
        [Required]
        [StringLength(maximumLength: 70)]
        public string DepartmentName { get; set; }
        public List<TeacherFaculty> TeacherFaculties { get; set; }
    }
}
