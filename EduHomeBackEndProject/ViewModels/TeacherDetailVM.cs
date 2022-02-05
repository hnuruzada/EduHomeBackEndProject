using EduHomeBackEndProject.Models;
using System.Collections.Generic;

namespace EduHomeBackEndProject.ViewModels
{
    public class TeacherDetailVM
    {
        public Teacher Teacher { get; set; }
        public List<TeacherHobbie> TeacherHobbies { get; set; }
        public List<Hobbie> Hobbie { get; set; }
        public List<TeacherFaculty> TeacherFaculties { get; set; }
        public List<Faculty> Faculties { get; set; }
        public List<TeacherPosition> TeacherPositions { get; set; }

        public List<Position> Positions { get; set; }
        public List<SocialMedia> SocialMedias { get; set; }
        public List<Skill> Skills { get; set; }
        
    }
}
