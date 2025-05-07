using System.Collections.Generic;

namespace OnlineTestForCheckingKnowledge.ViewModels.Admin
{
    public class EditUserRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<string> UserRoles { get; set; }
        public List<RoleViewModel> AllRoles { get; set; }
    }

    public class RoleViewModel
    {
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
}