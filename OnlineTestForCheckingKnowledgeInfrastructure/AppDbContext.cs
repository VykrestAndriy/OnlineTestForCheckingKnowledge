using Microsoft.EntityFrameworkCore;
using OnlineTestForCheckingKnowledge.Data.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace OnlineTestForCheckingKnowledge.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
    }
}