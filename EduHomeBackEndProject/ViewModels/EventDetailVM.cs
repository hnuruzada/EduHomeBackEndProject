using EduHomeBackEndProject.Models;
using System.Collections.Generic;

namespace EduHomeBackEndProject.ViewModels
{
    public class EventDetailVM
    {
        public Event Event { get; set; }
        public List<Event> Events { get; set; }
        public List<Blog>  Blogs { get; set; }
        public List<EventSpeaker> EventSpeakers { get; set; }
        public List<Speaker> Speakers { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
