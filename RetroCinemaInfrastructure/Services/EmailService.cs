using Microsoft.AspNetCore.Identity.UI.Services;

namespace RetroCinema.Services
{
    public class EmailService : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            System.Diagnostics.Debug.WriteLine("ВІДПРАВЛЕНО EMAIL");
            System.Diagnostics.Debug.WriteLine($"Кому: {email}");
            System.Diagnostics.Debug.WriteLine($"Тема: {subject}");
            System.Diagnostics.Debug.WriteLine($"Текст: {htmlMessage}");
            return Task.CompletedTask;
        }
    }
}