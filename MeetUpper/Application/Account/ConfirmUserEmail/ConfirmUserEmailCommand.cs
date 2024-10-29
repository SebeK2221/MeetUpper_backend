using Application.Account.Response;
using MediatR;

namespace Application.Account.ConfirmUserEmail;

public record ConfirmUserEmailCommand(
    Guid UserId,
    string Token
    ):IRequest<ConfirmUserEmailResponse>;