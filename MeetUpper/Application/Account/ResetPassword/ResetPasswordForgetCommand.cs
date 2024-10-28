using Application.Account.Response;
using Domain.Entities;
using MediatR;

namespace Application.Account.ResetPassword;

public record ResetPasswordForgetCommand(
    Guid UserId,
    string Token,
    string Password
    ):IRequest<ResetPasswordForgetResponse>;