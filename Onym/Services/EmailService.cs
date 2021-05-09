using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace Onym.Services
{
    public class EmailService
    {
        public async Task SendEmailAsync(string to, string subject, string message)
        {
            var emailMessage = new MimeMessage();
 
            emailMessage.From.Add(new MailboxAddress("ONYM", "a.d.maltsev81@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", to));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 465, true);
            await client.AuthenticateAsync("a.d.maltsev81@gmail.com", "59735651Google");
            await client.SendAsync(emailMessage);
 
            await client.DisconnectAsync(true);
        }
    }
}