using Application.Account.Response;
using Application.Persistance.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Account.SignIn;

public class SignInCommandHandler:IRequestHandler<SignInCommand,CreateUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public SignInCommandHandler(IUserRepository userRepository,UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<CreateUserResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmailAsync(request.Email, cancellationToken);
        await _userRepository.CheckPasswordAsync(user.Id, request.Password, cancellationToken);
        var token = _userRepository.GenerateTokenAsync(user);
        return new CreateUserResponse(await token);
    }
}