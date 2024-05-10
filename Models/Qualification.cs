namespace VTP_9.Models
{
    public class Qualification
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<Participant>? Participants { get; set; }
        public int UniversityId { get; set; }
        public University University { get; set; } = null!;
    }
}
