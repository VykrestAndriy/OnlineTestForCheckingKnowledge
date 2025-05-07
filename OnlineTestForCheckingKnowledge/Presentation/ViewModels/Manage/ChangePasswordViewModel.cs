using System.ComponentModel.DataAnnotations;

namespace OnlineTestForCheckingKnowledge.ViewModels.Manage
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Поточний пароль є обов'язковим.")]
        [DataType(DataType.Password)]
        [Display(Name = "Поточний пароль")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Новий пароль є обов'язковим.")]
        [StringLength(100, ErrorMessage = "Пароль має містити щонайменше {2} та щонайбільше {1} символів.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Новий пароль")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Підтвердження нового пароля")]
        [Compare("NewPassword", ErrorMessage = "Новий пароль та його підтвердження не співпадають.")]
        public string ConfirmPassword { get; set; }
    }
}