using System.ComponentModel.DataAnnotations;

namespace OnlineTestForCheckingKnowledge.Business.DTOs
{
    public class QuestionDto
    {
        [Required]
        [MaxLength(500)]
        public string Text { get; set; } = string.Empty;

        [Required]
        public int TestId { get; set; }
    }
}