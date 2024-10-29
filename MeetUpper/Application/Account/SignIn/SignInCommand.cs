using Application.Account.Response;
using MediatR;

namespace Application.Account.SignIn;

public sealed record SignInCommand(
    string Email,
    string Password
    ):IRequest<CreateUserResponse>;