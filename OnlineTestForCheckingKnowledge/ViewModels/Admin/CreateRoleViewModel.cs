using System.ComponentModel.DataAnnotations;

namespace OnlineTestForCheckingKnowledge.ViewModels.Admin
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "Назва ролі є обов'язковою.")]
        public string RoleName { get; set; }
    }
}