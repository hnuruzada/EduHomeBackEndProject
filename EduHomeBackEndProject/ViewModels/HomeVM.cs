using EduHomeBackEndProject.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace EduHomeBackEndProject.ViewModels
{
    public class HomeVM
    {
        public List<HeaderSlider> HeaderSliders { get; set; }
        public List<Course> Courses { get; set; }
        public List<Setting> Settings { get; set; }
        public List<Event> Events { get; set; }
        public List<NoticeBoard> NoticeBoards { get; set; }
        public List<Blog> Blogs { get; set; }

    }
}
