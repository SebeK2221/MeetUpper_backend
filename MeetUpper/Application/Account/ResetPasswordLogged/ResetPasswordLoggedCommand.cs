using Application.Account.Response;
using MediatR;

namespace Application.Account.ResetPasswordLogged;

public record ResetPasswordLoggedCommand(
    Guid UserId,
    string OldPassword,
    string NewPassword
    ):IRequest<ResetPasswordLoggedResponse>;