using Microsoft.EntityFrameworkCore;
using ResumeApp.Domain.Entities;


namespace ResumeApp.Infrastructure.Persistance
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Candidate> Candidates => Set<Candidate>();
        public DbSet<Degree> Degrees => Set<Degree>();
    }
}
