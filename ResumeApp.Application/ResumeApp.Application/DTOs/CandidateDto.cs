using System.ComponentModel.DataAnnotations;

namespace ResumeApp.Application.DTOs
{
    public class CandidateDto
    {
        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile must be 10 digits")]
        public string? Mobile { get; set; }

        public int? DegreeId { get; set; }

        public byte[]? CV { get; set; }
    }
}
