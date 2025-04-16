using System.ComponentModel.DataAnnotations;
using OnlineTestForCheckingKnowledge.Business.Validation;

namespace OnlineTestForCheckingKnowledge.Business.DTOs
{
    public class AnswerDto
    {
        [Required]
        [MaxLength(300)]
        [ValidAnswerText]
        public string Text { get; set; } = string.Empty;

        [Required]
        public bool IsCorrect { get; set; }

        [Required]
        public int QuestionId { get; set; }
    }
}