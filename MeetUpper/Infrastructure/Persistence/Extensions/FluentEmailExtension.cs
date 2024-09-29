using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence.Extensions
{
    public static class FluentEmailExtensions
    {
        public static IServiceCollection AddFluentEmail(this IServiceCollection services, IConfiguration configuration)
        {
            var emailSettings = configuration.GetSection("EmailSettings");
            var defaultFromEmail = emailSettings["DefaultFromEmail"];
            var host = emailSettings["Host"];
            var port = emailSettings.GetValue<int>("Port");
            var username = emailSettings["Username"];
            var password = emailSettings["Password"];
            var useSsl = emailSettings.GetValue<bool>("UseSsl");

            services.AddFluentEmail(defaultFromEmail)
                .AddSmtpSender(new SmtpClient
                {
                    Host = host,
                    Port = port,
                    EnableSsl = useSsl,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(username, password)
                });

            return services;
        }
    }
}