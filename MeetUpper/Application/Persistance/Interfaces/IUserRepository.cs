using Domain.Entities;

namespace Application.Persistance.Interfaces;


public interface IUserRepository
{
    Task<Guid> CreateUserAsync(Domain.Entities.User user, string password, CancellationToken cancellationToken);
    Task<User> GetUserByEmailAsync(string email,CancellationToken cancellationToken);
    Task<User> GetUserByIdAsync(Guid id,CancellationToken cancellationToken);
    Task<string> GenerateConfirmEmailTokenAsync(User user,CancellationToken cancellationToken);
    Task ConfirmUserEmail(Guid id, string token, CancellationToken cancellationToken);
    Task<string> GenerateResetPasswordTokenAsync(string email, CancellationToken cancellationToken);
}