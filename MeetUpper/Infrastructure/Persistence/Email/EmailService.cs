using FluentEmail.Core;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using MailKit.Net.Smtp;

namespace Infrastructure.Persistence.Email
{
    public class EmailService : IEmailService
    {
        private readonly IFluentEmailFactory _fluentEmailFactory;

        public EmailService(IFluentEmailFactory fluentEmailFactory)
        {
            _fluentEmailFactory = fluentEmailFactory;
        }

        public async Task Send(EmailMessageModel emailMessageModel)
        {
                var response = await _fluentEmailFactory.Create().To(emailMessageModel.ToAddress)
                    .Subject(emailMessageModel.Subject)
                    .Body(emailMessageModel.Body, true)
                    .SendAsync();
        }
    }
}