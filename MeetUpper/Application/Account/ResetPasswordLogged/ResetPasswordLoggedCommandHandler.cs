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
        throw new NotImplementedException();
    }
}