using Application.Account.ConfirmUserEmail;
using Application.Account.CreateUser;
using Application.Account.SendResetPasswordEmail_Forgot_Password_;
using Application.Account.SignIn;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost("sing-up")]
        public async Task<IActionResult> SignUp (CreateUserCommand command, CancellationToken cancellationToken)
        {
            var res = await _mediator.Send(command, cancellationToken);
            return Ok(res);
        }

        [HttpPost("sing-in")]
        public async Task<IActionResult> SignIn(SignInCommand command, CancellationToken cancellationToken)
        {
            var res = await _mediator.Send(command, cancellationToken);
            return Ok(res);
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmUserEmail(ConfirmUserEmailCommand command,
            CancellationToken cancellationToken)
        {
            var res = await _mediator.Send(command, cancellationToken);
            return Ok(res);
        }
        
        [HttpPost("forgot-password")]
        public async Task<IActionResult> SendResetPasswordEmaill(SendResetPasswordCommand command,
            CancellationToken cancellationToken)
        {
            var res = await _mediator.Send(command, cancellationToken);
            return Ok(res);
        }
    }
}