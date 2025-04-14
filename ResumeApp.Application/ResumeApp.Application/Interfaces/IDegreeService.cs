using ResumeApp.Application.DTOs;
using ResumeApp.Domain.Entities;


namespace ResumeApp.Application.Interfaces
{
    public interface IDegreeService
    {
        Task<List<Degree>> GetAllAsync();
        Task<Degree?> GetByIdAsync(int id);
        Task<Degree> CreateAsync(DegreeDto dto);
        Task<bool> UpdateAsync(int id, DegreeDto dto);
        Task<bool> DeleteIfUnusedAsync(int id);
    }
}
