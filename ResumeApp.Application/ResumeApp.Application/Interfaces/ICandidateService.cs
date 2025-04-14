using ResumeApp.Application.DTOs;


namespace ResumeApp.Application.Interfaces
{
    public interface ICandidateService
    {
        Task<List<CandidateResponseDto>> GetAllAsync();
        Task<CandidateResponseDto?> GetByIdAsync(int id);
        Task<CandidateResponseDto> CreateAsync(CandidateDto dto);
        Task<bool> UpdateAsync(int id, CandidateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
