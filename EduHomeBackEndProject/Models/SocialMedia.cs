using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduHomeBackEndProject.Models
{
    public class SocialMedia
    {
        public int Id { get; set; }
       
        [StringLength(maximumLength:50)]
        public string SocialIcon { get; set; }
        
        [StringLength(maximumLength: 50)]
        public string SocialUrl { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
