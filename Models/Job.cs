namespace VTP_9.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<Participant> ?Participants { get; set; }
    }
}
