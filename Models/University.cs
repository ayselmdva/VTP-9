namespace VTP_9.Models
{
    public class University
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<Qualification>? Qualifications { get; set; }
        public List<Participant>? Participants { get; set; }
    }
}
