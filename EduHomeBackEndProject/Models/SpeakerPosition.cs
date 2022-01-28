namespace EduHomeBackEndProject.Models
{
    public class SpeakerPosition
    {
        public int Id { get; set; }
        public int SpeakerId { get; set; }
        public Speaker Speaker { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}
