using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using MailKit.Net.Smtp;

namespace Infrastructure.Persistence.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpConfiguration _smtpConfiguration;

        public EmailSender(SmtpConfiguration smtpConfiguration)
        {
            _smtpConfiguration = smtpConfiguration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailMessage = CreateMessage(email, htmlMessage, subject);

            using var client = new SmtpClient();
            await client.ConnectAsync(_smtpConfiguration.SMTPHost, _smtpConfiguration.SMTPPort, useSsl: true);
            await client.AuthenticateAsync(_smtpConfiguration.SMTPUser, _smtpConfiguration.SMTPPass);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }

        private MimeMessage CreateMessage(string email, string body, string subject)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_smtpConfiguration.SMTPFrom));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };
            return message;
        }
    }
}