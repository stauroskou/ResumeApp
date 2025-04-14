using Microsoft.EntityFrameworkCore;
using ResumeApp.Application.DTOs;
using ResumeApp.Application.Interfaces;
using ResumeApp.Domain.Entities;
using ResumeApp.Infrastructure.Persistance;

namespace ResumeApp.Application.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly AppDbContext _context;

        public CandidateService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CandidateResponseDto>> GetAllAsync()
        {
            return await _context.Candidates
                .Include(c => c.Degree)
                .Select(c => new CandidateResponseDto
                {
                    Id = c.Id,
                    FullName = $"{c.FirstName} {c.LastName}",
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    Mobile = c.Mobile,
                    DegreeName = c.Degree != null ? c.Degree.Name : null,
                    CreationTime = c.CreationTime,
                    CV = c.CV
                })
                .ToListAsync();
        }

        public async Task<CandidateResponseDto?> GetByIdAsync(int id)
        {
            var candidate = await _context.Candidates.Include(c => c.Degree).FirstOrDefaultAsync(c => c.Id == id);
            if (candidate == null) return null;

            return new CandidateResponseDto
            {
                Id = candidate.Id,
                FullName = $"{candidate.FirstName} {candidate.LastName}",
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                Email = candidate.Email,
                Mobile = candidate.Mobile,
                DegreeName = candidate.Degree?.Name,
                CreationTime = candidate.CreationTime
            };
        }

        public async Task<CandidateResponseDto> CreateAsync(CandidateDto dto)
        {
            var candidate = new Candidate
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Mobile = dto.Mobile,
                DegreeId = dto.DegreeId,
                CV = dto.CV
            };

            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(candidate.Id) ?? throw new Exception("Error retrieving newly created candidate");
        }

        public async Task<bool> UpdateAsync(int id, CandidateDto dto)
        {
            var candidate = await _context.Candidates.FindAsync(id);
            if (candidate == null) return false;

            candidate.FirstName = dto.FirstName;
            candidate.LastName = dto.LastName;
            candidate.Email = dto.Email;
            candidate.Mobile = dto.Mobile;
            candidate.DegreeId = dto.DegreeId;
            candidate.CV = dto.CV;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var candidate = await _context.Candidates.FindAsync(id);
            if (candidate == null) return false;

            _context.Candidates.Remove(candidate);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
