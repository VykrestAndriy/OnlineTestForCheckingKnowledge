using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineTestForCheckingKnowledge.Data.Entities; 

namespace OnlineTestForCheckingKnowledge.Data.Entities
{
    public class Answer : IBaseEntity 
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