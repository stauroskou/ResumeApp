namespace ResumeApp.Domain.Entities
{
    public class Candidate
    {
        public int Id { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Mobile { get; set; }
        public int? DegreeId { get; set; }
        public Degree? Degree { get; set; }
        public byte[]? CV { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;
    }
}
