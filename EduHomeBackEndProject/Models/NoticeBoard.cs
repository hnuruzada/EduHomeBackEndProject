using System;
using System.ComponentModel.DataAnnotations;

namespace EduHomeBackEndProject.Models
{
    public class NoticeBoard
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [StringLength(maximumLength: 350)]
        public string Description { get; set; }
        
        
    }
}
