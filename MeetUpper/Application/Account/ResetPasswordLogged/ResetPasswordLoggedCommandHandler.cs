using Application.Account.Response;
using Application.Persistance.Interfaces;
using MediatR;

namespace Application.Account.ResetPasswordLogged;

public class ResetPasswordLoggedCommandHandler:IRequestHandler<ResetPasswordLoggedCommand,ResetPasswordLoggedResponse>
{
    private readonly IUserRepository _userRepository;

    public ResetPasswordLoggedCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<ResetPasswordLoggedResponse> Handle(ResetPasswordLoggedCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.CheckPasswordAsync(request.UserId, request.OldPassword, cancellationToken);
        await _userRepository.ResetPasswordLogged(request.UserId, request.OldPassword, request.NewPassword,
            cancellationToken);
        return new ResetPasswordLoggedResponse("Hasło zostało pomyślnie zmienione");
    }
}