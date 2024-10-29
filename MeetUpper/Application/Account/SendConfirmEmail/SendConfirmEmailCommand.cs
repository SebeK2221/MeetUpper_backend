using MediatR;

namespace Application.Account.SendConfirmEmail;

public sealed record SendConfirmEmailCommand(
    Guid UserId):IRequest;