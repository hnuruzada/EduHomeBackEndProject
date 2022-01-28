using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduHomeBackEndProject.Models
{
    public class Position
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:70)]
        public string Name { get; set; }
        public List<TeacherPosition> TeacherPositions { get; set; }
        public List<Student> Students { get; set; }
        public List<SpeakerPosition> SpeakerPositions { get; set; }
    }
}
