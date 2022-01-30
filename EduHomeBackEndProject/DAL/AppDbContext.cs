using EduHomeBackEndProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EduHomeBackEndProject.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<HeaderSlider> HeaderSliders{ get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CourseFeature> CourseFeatures { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseTag> CourseTags { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventSpeaker> EventSpeakers { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<FooterSocialMedia> FooterSocialMedias { get; set; }
        public DbSet<Hobbie> Hobbies { get; set; }
        public DbSet<NoticeBoard> NoticeBoards { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<SpeakerPosition> SpeakerPositions { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherFaculty> TeacherFaculties { get; set; }
        public DbSet<TeacherHobbie> TeacherHobbies { get; set; }
        public DbSet<TeacherPosition> TeacherPositions { get; set; }
        public DbSet<TeacherSkill> TeacherSkills { get; set; }





    }
}
