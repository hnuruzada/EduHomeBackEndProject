using EduHomeBackEndProject.Models;
using System.Collections.Generic;

namespace EduHomeBackEndProject.ViewModels
{
    public class CourseDetailVM
    {
        public Course Course { get; set; }
        public List<Course> Courses { get; set; }
        
        public List<Blog> Blogs { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Category> Categories { get; set; }
        public List<CourseFeature> CourseFeatures { get; set; }
        public List<Comment> Comments { get; set; }


    }
}
