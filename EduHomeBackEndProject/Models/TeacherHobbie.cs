namespace EduHomeBackEndProject.Models
{
    public class TeacherHobbie
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int HobbieId { get; set; }
        public Hobbie Hobbie { get; set; }
    }
}
