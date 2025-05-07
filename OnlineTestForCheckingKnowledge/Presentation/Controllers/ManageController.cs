using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineTestForCheckingKnowledge.Data.Entities; // Додайте цей using
using OnlineTestForCheckingKnowledge.ViewModels.Manage;
using System.Threading.Tasks;
using OnlineTestForCheckingKnowledge.ViewModels.Account;

[Authorize]
public class ManageController : Controller
{
    private readonly UserManager<User> _userManager; // Змініть тип на вашу сутність User
    private readonly SignInManager<User> _signInManager; // Змініть тип на вашу сутність User

    public ManageController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public async Task<IActionResult> EditProfile()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Не вдалося завантажити користувача з ID '{_userManager.GetUserId(User)}'.");
        }

        var model = new EditProfileViewModel
        {
            Email = user.Email
            // Додайте інші властивості профілю, які ви хочете редагувати
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProfile(EditProfileViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Не вдалося завантажити користувача з ID '{_userManager.GetUserId(User)}'.");
            }

            user.Email = model.Email;
            user.UserName = model.Email;
            // Оновіть інші властивості профілю користувача тут

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Профіль успішно оновлено.";
                return RedirectToAction(nameof(EditProfile));
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> ChangePassword()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Не вдалося завантажити користувача з ID '{_userManager.GetUserId(User)}'.");
        }

        var hasPassword = await _userManager.HasPasswordAsync(user);
        if (!hasPassword)
        {
            return RedirectToAction(nameof(SetPassword));
        }

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Не вдалося завантажити користувача з ID '{_userManager.GetUserId(User)}'.");
        }

        var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
        if (changePasswordResult.Succeeded)
        {
            await _signInManager.RefreshSignInAsync(user);
            TempData["SuccessMessage"] = "Пароль успішно змінено.";
            return RedirectToAction(nameof(ChangePassword));
        }
        foreach (var error in changePasswordResult.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            ModelState.AddModelError(string.Empty, "Електронна пошта є обов'язковою.");
            return View();
        }

        var user = await _userManager.FindByEmailAsync(email);
        if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
        {
            TempData["ForgotPasswordMessage"] = "Інструкції для відновлення пароля відправлено на вашу електронну пошту.";
            return View();
        }

        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
        var callbackUrl = Url.Action(
            nameof(ResetPassword),
            "Account",
            new { userId = user.Id, code },
            protocol: HttpContext.Request.Scheme);

        // Приклад реалізації надсилання електронного листа: SendEmailAsync(email, "Відновлення пароля", $"Будь ласка, перейдіть за посиланням для відновлення пароля: <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>відновити пароль</a>");

        TempData["ForgotPasswordMessage"] = "Інструкції для відновлення пароля відправлено на вашу електронну пошту.";
        return View();
    }

    [HttpGet]
    public IActionResult ResetPassword(string code = null)
    {
        return code == null ? View("Error") : View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
        if (result.Succeeded)
        {
            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
        return View();
    }

    [HttpGet]
    public IActionResult ResetPasswordConfirmation()
    {
        return View();
    }

    [HttpGet]
    public IActionResult SetPassword()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SetPassword(SetPasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Не вдалося завантажити користувача з ID '{_userManager.GetUserId(User)}'.");
        }

        var addPasswordResult = await _userManager.AddPasswordAsync(user, model.NewPassword);
        if (addPasswordResult.Succeeded)
        {
            await _signInManager.RefreshSignInAsync(user);
            TempData["SuccessMessage"] = "Пароль успішно встановлено.";
            return RedirectToAction(nameof(SetPassword));
        }
        foreach (var error in addPasswordResult.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
        return View(model);
    }
}