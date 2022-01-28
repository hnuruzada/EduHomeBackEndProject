using System.ComponentModel.DataAnnotations;

namespace EduHomeBackEndProject.Models
{
    public class HeaderSlider
    {
        public int Id { get; set; }
        [StringLength(maximumLength:70)]
        [Required]
        public string Image { get; set; }
        [StringLength(maximumLength: 70)]

        public string Title { get; set; }
        [StringLength(maximumLength: 200)]

        public string Description { get; set; }
        public int Order { get; set; }
    }
}
