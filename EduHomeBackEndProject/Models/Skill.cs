using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduHomeBackEndProject.Models
{
    public class Skill
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:60)]
        public string Name { get; set; }
        [Required]
        public int DegreePoint { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
