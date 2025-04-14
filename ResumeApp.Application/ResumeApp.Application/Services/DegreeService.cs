using Microsoft.EntityFrameworkCore;
using ResumeApp.Application.DTOs;
using ResumeApp.Application.Interfaces;
using ResumeApp.Domain.Entities;
using ResumeApp.Infrastructure.Persistance;

namespace ResumeApp.Application.Services
{
    public class DegreeService : IDegreeService
    {
        private readonly AppDbContext _context;

        public DegreeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Degree>> GetAllAsync()
        {
            return await _context.Degrees.ToListAsync();
        }

        public async Task<Degree?> GetByIdAsync(int id)
        {
            return await _context.Degrees.FindAsync(id);
        }

        public async Task<Degree> CreateAsync(DegreeDto dto)
        {
            var degree = new Degree { Name = dto.Name };
            _context.Degrees.Add(degree);
            await _context.SaveChangesAsync();
            return degree;
        }

        public async Task<bool> UpdateAsync(int id, DegreeDto dto)
        {
            var degree = await _context.Degrees.FindAsync(id);
            if (degree == null) return false;

            degree.Name = dto.Name;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteIfUnusedAsync(int id)
        {
            var degree = await _context.Degrees.Include(d => d.Candidates).FirstOrDefaultAsync(d => d.Id == id);
            if (degree == null || (degree.Candidates != null && degree.Candidates.Any()))
                return false;

            _context.Degrees.Remove(degree);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
