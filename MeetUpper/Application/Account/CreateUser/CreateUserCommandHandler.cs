using Application.Account.Response;
using Application.Account.SendConfirmEmail;
using Application.Persistance.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Account.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand,CreateUserResponse>
{
    private readonly IMediator _mediator;
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IMediator mediator,IUserRepository userRepository)
    {
        _mediator = mediator;
        _userRepository = userRepository;
    }

    public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            UserName = request.Email,
            Email = request.Email,
            Name = request.Name,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber
        };
        var userId = await _userRepository.CreateUserAsync(user, request.Password,cancellationToken);
        await _mediator.Send(new SendConfirmEmailCommand(userId), cancellationToken);
        return new CreateUserResponse(message: "Utworzono użytkownika");
    }
}