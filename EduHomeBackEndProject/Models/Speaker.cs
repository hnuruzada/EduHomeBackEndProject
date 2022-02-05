﻿using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduHomeBackEndProject.Models
{
    public class Speaker
    {
        public int Id { get; set; }
        
        [StringLength(maximumLength:70)]
        public string Image { get; set; }
        [Required]
        [StringLength(maximumLength: 30)]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 30)]
        public string Surname { get; set; }
        public List<SpeakerPosition> SpeakerPositions { get; set; }
        public List<EventSpeaker> EventSpeakers { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [NotMapped]
        public List<int> PositionIds { get; set; }
    }
}
