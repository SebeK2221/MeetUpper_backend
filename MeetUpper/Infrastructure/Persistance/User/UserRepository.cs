using Application.Persistance.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Exception = System.Exception;

namespace Infrastructure.Persistance.User;

public class UserRepository:IUserRepository
{
    private readonly MeetUpperDbContext _context;
    private readonly UserManager<Domain.Entities.User> _userManager;

    public UserRepository(MeetUpperDbContext context,UserManager<Domain.Entities.User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    
    public async Task<Guid> CreateUserAsync (Domain.Entities.User user, string password, CancellationToken cancellationToken)
    {
        var isUserEmailExists = await _userManager.Users.AnyAsync(x => x.Email == user.Email, cancellationToken);
        if (isUserEmailExists == true)
        {
            throw new Exception($"Użytkownik o takim mailu już istnieje");
        }

        var createUser = await _userManager.CreateAsync(user, password);
        if (!createUser.Succeeded)
        {
            throw new Exception($"Błąd przy tworzeniu użytkownika: {string.Join(", ", createUser.Errors.Select(e => e.Description))}");
        }
        await _context.SaveChangesAsync(cancellationToken);
        return user.Id;
    }

    public async Task<Domain.Entities.User> GetUserByEmailAsync(string email,CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            throw new Exception($"Nie znaleziono użytkownika");
        }

        return user;
    }

    public async Task<Domain.Entities.User> GetUserByIdAsync(Guid id,CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            throw new Exception($"Nie znaleziono użytkonika");
        }

        return user;
    }

    public async Task<string> GenerateConfirmEmailTokenAsync(Domain.Entities.User user,CancellationToken cancellationToken)
    {
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        return token;
    }

    public async Task ConfirmUserEmail(Guid id, string token, CancellationToken cancellationToken)
    {
        var user = await GetUserByIdAsync(id, cancellationToken);
        var confirm = await _userManager.ConfirmEmailAsync(user, token);
        if (!confirm.Succeeded)
        {
            throw new Exception("Użytkownik o takim mailu nie istnieje");
        }
    }

    public async Task<string> GenerateResetPasswordTokenAsync(string email, CancellationToken cancellationToken)
    {
        var user = await GetUserByEmailAsync(email, cancellationToken);
        var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        return resetPasswordToken;
    }
}