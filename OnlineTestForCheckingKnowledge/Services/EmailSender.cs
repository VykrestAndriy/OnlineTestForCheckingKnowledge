using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;
namespace OnlineTestForCheckingKnowledge.Services;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        // Реалізація логіки відправки електронної пошти
        // Використання свого сервісу для відправки email (наприклад, SMTP)

        System.Diagnostics.Debug.WriteLine($"Відправка email на: {email}, Тема: {subject}, Зміст: {htmlMessage}");
        return Task.CompletedTask;
    }
}