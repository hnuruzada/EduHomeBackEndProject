using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduHomeBackEndProject.Models
{
    public class HeaderSlider
    {
        public int Id { get; set; }
        [StringLength(maximumLength:70)]
        
        public string Image { get; set; }
       
        [StringLength(maximumLength: 70)]

        public string Title { get; set; }
        [StringLength(maximumLength: 200)]

        public string Description { get; set; }
        public int Order { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        
    }
}
