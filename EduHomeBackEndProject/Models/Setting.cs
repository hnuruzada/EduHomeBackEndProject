using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduHomeBackEndProject.Models
{
    public class Setting
    {

        public int Id { get; set; }
       
        [StringLength(maximumLength: 50)]
        public string HeaderLogo { get; set; }
        
        [StringLength(maximumLength: 50)]
        public string FooterLogo { get; set; }
        
        [StringLength(maximumLength: 50)]
        public string SearchIcon { get; set; }
        [StringLength(maximumLength: 50)]
        public string PhoneNumber1 { get; set; }
        [StringLength(maximumLength: 50)]
        public string PhoneNumber2 { get; set; }
       
        [StringLength(maximumLength: 50)]
        public string Mail { get; set; }
        [StringLength(maximumLength: 250)]

        public string Address { get; set; }
        [StringLength(maximumLength: 50)]

        public string AboutContentTitle { get; set; }
        [StringLength(maximumLength: 500)]

        public string AboutContentDesc { get; set; }
        [StringLength(maximumLength: 70)]

        public string AboutContentImage { get; set; }
        [StringLength(maximumLength: 150)]

        public string AboutContentUrl { get; set; }
        
        [StringLength(maximumLength: 50)]

        public string WebAddress{ get; set; }
        [StringLength(maximumLength: 500)]

        public string FooterDescription { get; set; }
        [StringLength(maximumLength: 250)]
        public string VideoTourUrl { get; set; }
        public List<FooterSocialMedia> FooterSocialMedias { get; set; }

        [NotMapped]
        public IFormFile HeaderLogoFile { get; set; }
        [NotMapped]
        
        public IFormFile FooterLogoFile { get; set; }
        [NotMapped]

        public IFormFile AboutImgFile { get; set; }



    }
}
