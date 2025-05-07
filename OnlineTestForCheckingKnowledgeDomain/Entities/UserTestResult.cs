using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineTestForCheckingKnowledge.Data.Entities
{
    public class UserTestResult
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Test")]
        public int TestId { get; set; }
        public virtual Test Test { get; set; }

        public int CorrectAnswers { get; set; }
        public int TotalQuestions { get; set; }
    }
}