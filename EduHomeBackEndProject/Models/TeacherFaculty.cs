namespace EduHomeBackEndProject.Models
{
    public class TeacherFaculty
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; }
    }
}
