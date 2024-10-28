using Application.Account.Response;
using MediatR;

namespace Application.Account.SendResetPasswordEmail_Forgot_Password_;

public record SendResetPasswordCommand(
    string Email
    ):IRequest<SendResetPasswordResponse>;