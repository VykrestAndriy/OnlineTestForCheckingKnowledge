using System.ComponentModel.DataAnnotations;

namespace OnlineTestForCheckingKnowledge.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Електронна пошта є обов'язковою.")]
        [EmailAddress(ErrorMessage = "Невірний формат електронної пошти.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пароль є обов'язковим.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Запам'ятати мене?")]
        public bool RememberMe { get; set; }
    }
}