using System.ComponentModel.DataAnnotations;

namespace OnlineTestForCheckingKnowledge.Data.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        public List<UserTestResult> TestResults { get; set; } = new();
    }
}
