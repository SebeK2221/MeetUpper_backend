using Application.Account.Response;
using Application.Persistance.Interfaces;
using MediatR;

namespace Application.Account.ConfirmUserEmail;

public class ConfirmUserEmailCommandHandler:IRequestHandler<ConfirmUserEmailCommand,ConfirmUserEmailResponse>
{
    private readonly IUserRepository _userRepository;

    public ConfirmUserEmailCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<ConfirmUserEmailResponse> Handle(ConfirmUserEmailCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.ConfirmUserEmail(request.UserId, request.Token, cancellationToken);
        return new ConfirmUserEmailResponse("Twój adres email został potwierdzony!");
    }
}