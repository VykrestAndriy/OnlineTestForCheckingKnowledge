using Microsoft.EntityFrameworkCore;
using OnlineTestForCheckingKnowledge.Data.Entities;

namespace OnlineTestForCheckingKnowledge.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Налаштування автоматичної генерації ID для Test
            builder.Entity<Test>()
                .HasKey(t => t.Id);
            builder.Entity<Test>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Test>().HasData(
                new Test { Id = 49, Title = "Тест 1", Name = "Тест 1" }
            );

            builder.Entity<Question>().HasData(
                new Question { Id = 1, TestId = 49, Text = "Що таке ASP.NET MVC?" }
            );

            builder.Entity<Answer>().HasData(
                new Answer { Id = 1, QuestionId = 1, Text = "Фреймворк для побудови веб-застосунків на основі моделі Model-View-Controller.", IsCorrect = true },
                new Answer { Id = 2, QuestionId = 1, Text = "Бібліотека класів для роботи з базами даних.", IsCorrect = false },
                new Answer { Id = 3, QuestionId = 1, Text = "Мова програмування для створення клієнтських сценаріїв.", IsCorrect = false }
            );

            builder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Question>()
                .HasOne(q => q.CorrectAnswer)
                .WithOne()
                .HasForeignKey<Question>(q => q.CorrectAnswerId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}