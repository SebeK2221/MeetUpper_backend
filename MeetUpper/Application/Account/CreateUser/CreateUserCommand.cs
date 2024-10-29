using Application.Account.Response;
using MediatR;

namespace Application.Account.CreateUser;

public sealed record CreateUserCommand(
    string Name,
    string LastName,
    string Email,
    string Password,
    string RepeatPassword,
    string PhoneNumber
    ):IRequest<CreateUserResponse>;