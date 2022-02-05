using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduHomeBackEndProject.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Image { get; set; }
        [Required]
        [StringLength(maximumLength:250)]
        public string Title { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [StringLength(maximumLength: 1000)]
        public string Description { get; set; }
        public List<Comment> Comments { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
