namespace EduHomeBackEndProject.Models
{
    public class TeacherPosition
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}
