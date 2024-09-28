using System.Net.Mail;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using MimeKit.Text;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;


namespace Infrastructure.Persistence.Email;

public class EmailService : IEmailSender
{
    private readonly SmtpConfiguration _smtpConfiguration;

    public EmailService(SmtpConfiguration smtpConfiguration)
    {
        _smtpConfiguration = smtpConfiguration;
    }
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var emailMessage = CreateMessage(email, htmlMessage, subject);

        using var client = new SmtpClient();
        {
            await client.ConnectAsync(_smtpConfiguration.SMTPHost, _smtpConfiguration.SMTPPort);
            await client.AuthenticateAsync(_smtpConfiguration.SMTPUser, _smtpConfiguration.SMTPPass);
            var status = await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }

    private MimeMessage CreateMessage(string email, string body, string subject)
    {
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(_smtpConfiguration.SMTPFrom));
        message.To.Add(MailboxAddress.Parse(email));
        message.Subject = subject;
        message.Body = new TextPart(TextFormat.Html) { Text = body };
        return message;
    }
}