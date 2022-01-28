namespace EduHomeBackEndProject.Models
{
    public class TeacherSocial
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int SocialMediaId { get; set; }
        public SocialMedia SocialMedia { get; set; }
    }
}
