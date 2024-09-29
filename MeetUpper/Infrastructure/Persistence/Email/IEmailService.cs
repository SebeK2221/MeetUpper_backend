namespace Infrastructure.Persistence.Email;

public interface IEmailService
{
    Task Send(EmailMessageModel emailMessage);
}