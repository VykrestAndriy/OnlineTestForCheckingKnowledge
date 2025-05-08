using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace OnlineTestForCheckingKnowledge.Services;

public class EmailSender : IEmailSender
{
    private readonly IConfiguration _configuration;

    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var smtpSettings = _configuration.GetSection("SmtpSettings");
        var host = smtpSettings["Host"];
        var port = int.Parse(smtpSettings["Port"] ?? "587");
        var username = smtpSettings["Username"];
        var password = smtpSettings["Password"];
        var senderEmail = smtpSettings["SenderEmail"];

        using (var client = new SmtpClient(host, port))
        {
            client.EnableSsl = smtpSettings.GetValue<bool>("EnableSsl");
            client.Credentials = new NetworkCredential(username, password);

            var mailMessage = new MailMessage(senderEmail, email, subject, htmlMessage);
            mailMessage.IsBodyHtml = true;

            try
            {
                await client.SendMailAsync(mailMessage);
                System.Diagnostics.Debug.WriteLine($"Email успішно відправлено на: {email}, Тема: {subject}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Помилка відправки email на: {email}, Тема: {subject}, Помилка: {ex.Message}");
                
                throw; 
            }
        }
    }
}