using System.ComponentModel.DataAnnotations;

namespace OnlineTestForCheckingKnowledge.ViewModels.Manage
{
    public class EditProfileViewModel
    {
        [Display(Name = "Ім'я")]
        public string FirstName { get; set; }

        [Display(Name = "Прізвище")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Електронна пошта є обов'язковою.")]
        [EmailAddress(ErrorMessage = "Невірний формат електронної пошти.")]
        [Display(Name = "Електронна пошта")]
        public string Email { get; set; }
    }
}