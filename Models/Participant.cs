namespace VTP_9.Models
{
    public class Participant
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public int Age {  get; set; }
        public int JobId { get; set; }
        public Job Job { get; set; } = null!;
        public int? UniversityId { get; set; }
        public University? University { get; set; }
        public int QualificationId { get; set; }
        public Qualification Qualification { get; set; } = null!;
    }
}
