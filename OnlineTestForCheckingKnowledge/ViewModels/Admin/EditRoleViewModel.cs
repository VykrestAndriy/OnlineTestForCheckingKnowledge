using System.ComponentModel.DataAnnotations;

namespace OnlineTestForCheckingKnowledge.ViewModels.Admin
{
    public class EditRoleViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Назва ролі є обов'язковою.")]
        [Display(Name = "Нова назва ролі")]
        public string NewRoleName { get; set; }
    }
}