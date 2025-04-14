using System.ComponentModel.DataAnnotations;

namespace ResumeApp.Application.DTOs
{
    public class DegreeDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
