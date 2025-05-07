using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineTestForCheckingKnowledge.Data.Entities;
using OnlineTestForCheckingKnowledge.ViewModels.Admin;
using System.Linq;
using System.Threading.Tasks;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AdminController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IActionResult> UserList()
    {
        var users = await _userManager.Users.ToListAsync();
        return View(users);
    }

    public async Task<IActionResult> EditUserRoles(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var userRoles = (await _userManager.GetRolesAsync(user)).ToList();
        var allRoles = _roleManager.Roles.Select(r => r.Name).ToList();
        var model = new EditUserRolesViewModel
        {
            UserId = user.Id,
            UserName = user.UserName,
            UserRoles = userRoles,
            AllRoles = allRoles.Select(roleName => new RoleViewModel { RoleName = roleName, IsSelected = userRoles.Contains(roleName) }).ToList()
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditUserRoles(EditUserRolesViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user == null)
        {
            return NotFound();
        }

        var currentRoles = await _userManager.GetRolesAsync(user);
        var rolesToAdd = model.AllRoles.Where(r => r.IsSelected && !currentRoles.Contains(r.RoleName)).Select(r => r.RoleName).ToList();
        var rolesToRemove = model.AllRoles.Where(r => !r.IsSelected && currentRoles.Contains(r.RoleName)).Select(r => r.RoleName).ToList();

        var addResult = await _userManager.AddToRolesAsync(user, rolesToAdd);
        var removeResult = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

        if (addResult.Succeeded && removeResult.Succeeded)
        {
            return RedirectToAction(nameof(UserList));
        }
        else
        {
            ModelState.AddModelError("", "Не вдалося оновити ролі користувача.");
            return View(model);
        }
    }

    public async Task<IActionResult> RoleList()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        return View(roles);
    }

    public IActionResult CreateRole()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (!string.IsNullOrWhiteSpace(model.RoleName))
            {
                var roleExist = await _roleManager.RoleExistsAsync(model.RoleName);
                if (!roleExist)
                {
                    var roleResult = await _roleManager.CreateAsync(new IdentityRole(model.RoleName));
                    if (roleResult.Succeeded)
                    {
                        return RedirectToAction(nameof(RoleList));
                    }
                    else
                    {
                        foreach (var error in roleResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("RoleName", "Роль з такою назвою вже існує.");
                }
            }
        }
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteRole(string id)
    {
        if (!string.IsNullOrWhiteSpace(id))
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var roleResult = await _roleManager.DeleteAsync(role);
                if (roleResult.Succeeded)
                {
                    return RedirectToAction(nameof(RoleList));
                }
                else
                {
                    foreach (var error in roleResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
        }
        return RedirectToAction(nameof(RoleList));
    }

    [HttpGet]
    public IActionResult CreateUser()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateUser(CreateUserViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new User { UserName = model.UserName, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(UserList));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            return RedirectToAction(nameof(UserList));
        }
        else
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return RedirectToAction(nameof(UserList));
    }
}