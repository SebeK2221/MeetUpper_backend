using Application.Persistance.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Application.Account.SendConfirmEmail;

public class SendConfirmEmailCommandHandler:IRequestHandler<SendConfirmEmailCommand>
{
    private readonly IEmailSender _emailSender;
    private readonly IUserRepository _userRepository;

    public SendConfirmEmailCommandHandler(IEmailSender emailSender, IUserRepository userRepository)
    {
        _emailSender = emailSender;
        _userRepository = userRepository;
    }
    public async Task Handle(SendConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var userToConfirm = await _userRepository.GetUserByIdAsync(request.UserId,cancellationToken);
        var tokenToConfirmUserEmail = await _userRepository.GenerateConfirmEmailTokenAsync(userToConfirm,cancellationToken);
        
        var message = "sds" + tokenToConfirmUserEmail;
        await _emailSender.SendEmailAsync(userToConfirm.Email, "Kliknij w link aby aktywować konto", message);
    }
}