using System.ComponentModel.DataAnnotations;

namespace EduHomeBackEndProject.Models
{
    public class FooterSocialMedia
    {
        public int Id { get; set; }
        
        [StringLength(maximumLength:50)]

        public string Icon { get; set; }
       
        [StringLength(maximumLength: 120)]
        public string IconUrl { get; set; }
        public int SettingId { get; set; }
        public Setting Setting { get; set; }

    }
}