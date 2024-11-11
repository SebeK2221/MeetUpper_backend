using System.Net;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using Application.Persistance.Interfaces;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Exception = System.Exception;

namespace Infrastructure.Persistance.User;

public class UserRepository:IUserRepository
{
    private readonly MeetUpperDbContext _context;
    private readonly UserManager<Domain.Entities.User> _userManager;
    private readonly IConfiguration _configuration;

    public UserRepository(MeetUpperDbContext context,UserManager<Domain.Entities.User> userManager, IConfiguration configuration)
    {
        _context = context;
        _userManager = userManager;
        _configuration = configuration;
    }
    
    public async Task<string> GenerateTokenAsync(Domain.Entities.User user)
    {
        var secretKey = _configuration["Jwt:Secret"];
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var claims = new ClaimsIdentity(
        [
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("email_confirmed", user.EmailConfirmed.ToString())
        ]);

        var now = DateTime.UtcNow;
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claims,
            Expires = now.AddMinutes(_configuration.GetValue<int>("Jwt:ExpiresInMinutes")),
            SigningCredentials = credentials,
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };
        var handler = new JsonWebTokenHandler();
        var token = handler.CreateToken(tokenDescriptor);
        return token;
    }
    
    public async Task<Guid> CreateUserAsync (Domain.Entities.User user, string password, CancellationToken cancellationToken)
    {
        var isUserEmailExists = await _userManager.Users.AnyAsync(x => x.Email == user.Email, cancellationToken);
        if (isUserEmailExists == true)
        {
            throw new ConflictException($"Użytkownik o takim mailu już istnieje");
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
            throw new NotFoundException($"Nie znaleziono użytkownika");
        }

        return user;
    }

    public async Task<Domain.Entities.User> GetUserByIdAsync(Guid id,CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            throw new NotFoundException($"Nie znaleziono użytkonika");
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
            throw new BadRequestException("Nie udało się potwierdzić maila");
        }
    }

    public async Task<string> GenerateResetPasswordTokenAsync(string email, CancellationToken cancellationToken)
    {
        var user = await GetUserByEmailAsync(email, cancellationToken);
        var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        return resetPasswordToken;
    }

    public async Task ResetPasswordForgot(Guid id, string token, string password, CancellationToken cancellationToken)
    {
        var user = await GetUserByIdAsync(id, cancellationToken);
        var changePassword=await _userManager.ResetPasswordAsync(user, token, password);
        if (!changePassword.Succeeded)
        {
            throw new BadRequestException("Nie udało się zmienić hasła");
        }
    }

    public async Task ResetPasswordLogged(Guid id, string oldPassword, string newPassword, CancellationToken cancellationToken)
    {
        var user = await GetUserByIdAsync(id, cancellationToken);
        await CheckPasswordAsync(user.Id, oldPassword, cancellationToken);
        var changePassword = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        if (!changePassword.Succeeded)
        {
            throw new BadRequestException("Nie udało się zmienić hasła");
        }
    }

    public async Task CheckPasswordAsync(Guid id, string password, CancellationToken cancellationToken)
    {
        var user = await GetUserByIdAsync(id, cancellationToken);
        var checkPassword = await _userManager.CheckPasswordAsync(user, password);
        if (checkPassword is false)
        {
            throw new InvalidCredentialException("Podano niepoprawne hasło");
        }
    }
    

    public async Task test(string pass)
    {
        throw new BadRequestException("działa");
    }
}