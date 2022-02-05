using EduHomeBackEndProject.Models;
using System.Collections.Generic;

namespace EduHomeBackEndProject.ViewModels
{
    public class AboutVM
    {
        public List<Setting> Settings { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<NoticeBoard> NoticeBoards { get; set; }

    }
}
