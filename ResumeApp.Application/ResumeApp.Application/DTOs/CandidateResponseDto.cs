namespace ResumeApp.Application.DTOs
{
    public class CandidateResponseDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Mobile { get; set; }
        public string? DegreeName { get; set; }
        public DateTime CreationTime { get; set; }
        public byte[]? CV { get; set; }
    }
}
