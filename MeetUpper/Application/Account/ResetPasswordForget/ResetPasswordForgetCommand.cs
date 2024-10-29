using Application.Account.Response;
using MediatR;

namespace Application.Account.ResetPasswordForget;

public record ResetPasswordForgetCommand(
    Guid UserId,
    string Token,
    string Password
    ):IRequest<ResetPasswordForgetResponse>;