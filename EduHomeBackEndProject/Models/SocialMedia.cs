﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduHomeBackEndProject.Models
{
    public class SocialMedia
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:50)]
        public string SocialIcon { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string SocialUrl { get; set; }
        public List<SocialMedia> SocialMedias { get; set; }
    }
}
