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
            throw new Exception($"Użytkonik o takim mailu już istnieje");
        }

        var createUser = await _userManager.CreateAsync(user, password);
        if (!createUser.Succeeded)
        {
            throw new Exception($"Błąd przy tworzeniu użytkownika: {string.Join(", ", createUser.Errors.Select(e => e.Description))}");
        }
        await _context.SaveChangesAsync(cancellationToken);
        return user.Id;
    }

    public async Task<Domain.Entities.User> GetUserByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            throw new Exception($"Nie znaleziono użytkonika");
        }

        return user;
    }

    public async Task<Domain.Entities.User> GetUserByIdAsync(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            throw new Exception($"Nie znaleziono użytkonika");
        }

        return user;
    }

    public async Task<string> GenerateConfirmEmailTokenAsync(Guid id)
    {
        var user = await GetUserByIdAsync(id);
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        return token;
    }
}