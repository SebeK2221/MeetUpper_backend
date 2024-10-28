using Application.Account.Response;
using Application.Persistance.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Application.Account.SendResetPasswordEmail_Forgot_Password_;

public class SendResetPasswordCommandHandler:IRequestHandler<SendResetPasswordCommand,SendResetPasswordResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailSender _emailSender;

    public SendResetPasswordCommandHandler(IUserRepository userRepository,IEmailSender emailSender)
    {
        _userRepository = userRepository;
        _emailSender = emailSender;
    }
    public async Task<SendResetPasswordResponse> Handle(SendResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var tokenToResetPassword = await _userRepository.GenerateResetPasswordTokenAsync(request.Email, cancellationToken);
        var userToResetPassword = await _userRepository.GetUserByEmailAsync(request.Email, cancellationToken);
        var link = $"<a href=http://localhost:5000/forgot-password?token={tokenToResetPassword}&userId={userToResetPassword.Id}>Kliknij tutaj</a>";
        var message = "Link do resetu hasła" + link;
        await _emailSender.SendEmailAsync(request.Email, "Reset password", message);
        return new SendResetPasswordResponse("Link umożliwiający zmianę hasła został wysłany na twój adres email");
    }
}