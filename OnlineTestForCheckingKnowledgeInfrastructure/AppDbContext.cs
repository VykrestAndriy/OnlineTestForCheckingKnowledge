using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineTestForCheckingKnowledge.Data.Entities;

namespace OnlineTestForCheckingKnowledge.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<UserTestResult> UserTestResults { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Test>()
                .HasKey(t => t.Id);
            builder.Entity<Test>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();
            builder.Entity<Test>().HasData(new Test { Id = 49, Title = "Тест 1", Name = "Тест 1" });
            builder.Entity<Question>().HasData(new Question { Id = 1, TestId = 49, Text = "Що таке ASP.NET MVC?" });
            builder.Entity<Answer>().HasData(
                new Answer { Id = 1, QuestionId = 1, Text = "Фреймворк ...", IsCorrect = true },
                new Answer { Id = 2, QuestionId = 1, Text = "Бібліотека ...", IsCorrect = false },
                new Answer { Id = 3, QuestionId = 1, Text = "Мова ...", IsCorrect = false }
            );
            builder.Entity<Answer>().HasOne(a => a.Question).WithMany(q => q.Answers).HasForeignKey(a => a.QuestionId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Question>().HasOne(q => q.CorrectAnswer).WithOne().HasForeignKey<Question>(q => q.CorrectAnswerId).OnDelete(DeleteBehavior.SetNull);

            builder.Entity<UserTestResult>()
                .HasKey(utr => new { utr.UserId, utr.TestId });

            builder.Entity<UserTestResult>()
                .HasOne<User>()
                .WithMany(u => u.TestResults)
                .HasForeignKey(utr => utr.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserTestResult>()
                .HasOne<Test>()
                .WithMany()
                .HasForeignKey(utr => utr.TestId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<User>()
                .Property(u => u.FirstName)
                .IsRequired(false);

            builder.Entity<User>()
                .Property(u => u.LastName)
                .IsRequired(false);
        }
    }
}