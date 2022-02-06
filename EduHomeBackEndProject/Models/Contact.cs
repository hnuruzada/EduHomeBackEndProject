using System;

namespace EduHomeBackEndProject.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
    }
}
