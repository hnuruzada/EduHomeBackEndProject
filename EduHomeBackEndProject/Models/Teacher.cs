using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduHomeBackEndProject.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:30)]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 30)]
        public string Surname { get; set; }
        
        [StringLength(maximumLength: 150)]
        public string Image { get; set; }
        [Required]
        [StringLength(maximumLength: 500)]
        public string AboutMe { get; set; }
        [Required]
        
        [StringLength(maximumLength: 70)]
        public string Experience { get; set; }
        
        [StringLength(maximumLength: 70)]
        public string Degree { get; set; }
        
        [StringLength(maximumLength: 50)]
        public string Mail { get; set; }
        
        [StringLength(maximumLength: 50)]
        public string PhoneNumber { get; set; }
        public List<TeacherHobbie> TeacherHobbies { get; set; }
        public List<TeacherFaculty> TeacherFaculties { get; set; }
        public List<TeacherSkill> TeacherSkills { get; set; }
        public List<SocialMedia> SocialMedias { get; set; }
        public List<TeacherPosition> TeacherPositions { get; set; }
    }
}
