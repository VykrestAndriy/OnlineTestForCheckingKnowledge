using System.ComponentModel.DataAnnotations;

namespace OnlineTestForCheckingKnowledge.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Електронна пошта є обов'язковою.")]
        [EmailAddress]
        public string Email { get; set; }
    }
}