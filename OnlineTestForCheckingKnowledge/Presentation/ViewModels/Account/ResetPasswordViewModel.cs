using System.ComponentModel.DataAnnotations;

namespace OnlineTestForCheckingKnowledge.ViewModels.Account
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Електронна пошта є обов'язковою.")]
        [EmailAddress(ErrorMessage = "Невірний формат електронної пошти.")]
        [Display(Name = "Електронна пошта")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пароль є обов'язковим.")]
        [StringLength(100, ErrorMessage = "Пароль має містити щонайменше {2} та щонайбільше {1} символів.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Новий пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Підтвердження нового пароля")]
        [Compare("Password", ErrorMessage = "Новий пароль та його підтвердження не співпадають.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Code { get; set; }
    }
}