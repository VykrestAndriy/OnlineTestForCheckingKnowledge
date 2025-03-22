using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineTestForCheckingKnowledge.Data.Entities
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Text { get; set; } = string.Empty;

        public bool IsCorrect { get; set; }

        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
