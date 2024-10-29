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
        var link = $"<a href=http://localhost:5000/confirm-email?token={tokenToConfirmUserEmail}&userId={userToConfirm.Id}>Kliknij tutaj</a>";
        var message = "Link potwierdzający adres email" + link;
        await _emailSender.SendEmailAsync(userToConfirm.Email, "Kliknij w link aby aktywować konto", message);
    }
}