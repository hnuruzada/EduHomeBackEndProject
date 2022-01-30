using System.ComponentModel.DataAnnotations;

namespace EduHomeBackEndProject.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:25)]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 25)]
        public string Surname { get; set; }
        [Required]
        [StringLength(maximumLength: 350)]
        public string Info { get; set; }
        
        [StringLength(maximumLength: 70)]
        public string Image { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}
