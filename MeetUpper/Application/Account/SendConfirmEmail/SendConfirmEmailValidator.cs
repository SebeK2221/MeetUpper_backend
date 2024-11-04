using FluentValidation;

namespace Application.Account.SendConfirmEmail;

public class SendConfirmEmailValidator:AbstractValidator<SendConfirmEmailCommand>
{
    public SendConfirmEmailValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("ID użytkonika nie może być puste");
    }
}