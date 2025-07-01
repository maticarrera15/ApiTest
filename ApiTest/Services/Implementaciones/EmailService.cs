

using ApiTest.Services.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;

namespace ApiTest.Services.Implementaciones
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("maticarrera15@gmail.com"));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = body };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync("maticarrera15@gmail.com", "rxqdvuyetxfxhhar");
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
            //guardat en un secrets.json por ej.
            //psw generada en google rxqdvuyetxfxhhar
        }
    }
}
