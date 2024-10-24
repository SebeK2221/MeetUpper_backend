using Domain.Entities;

namespace Application.Persistance.Interfaces;


public interface IUserRepository
{
    Task<Guid> CreateUserAsync(Domain.Entities.User user, string password, CancellationToken cancellationToken);
    Task<User> GetUserByEmailAsync(string email);
    Task<User> GetUserByIdAsync(Guid id);
    Task<string> GenerateConfirmEmailTokenAsync(Guid id);
}