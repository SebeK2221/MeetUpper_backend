using Application.Account.Response;
using Application.Persistance.Interfaces;
using MediatR;

namespace Application.Account.ResetPasswordForget;

public class ResetPasswordForgetCommandHandler:IRequestHandler<ResetPasswordForgetCommand,ResetPasswordForgetResponse>
{
    private readonly IUserRepository _userRepository;

    public ResetPasswordForgetCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<ResetPasswordForgetResponse> Handle(ResetPasswordForgetCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.ResetPasswordForgot(request.UserId, request.Token, request.Password,cancellationToken);
        return new ResetPasswordForgetResponse("Twoje hasło zostało zmienione");
    }
}