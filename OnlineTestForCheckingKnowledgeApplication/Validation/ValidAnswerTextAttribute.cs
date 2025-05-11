using System.ComponentModel.DataAnnotations;

namespace OnlineTestForCheckingKnowledge.Business.Validation
{
    public class ValidAnswerTextAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var text = value as string;
            if (string.IsNullOrWhiteSpace(text))
            {
                return new ValidationResult("Answer text cannot be empty or whitespace.");
            }

            return ValidationResult.Success;
        }
    }
}