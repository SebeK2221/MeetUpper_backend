using FluentValidation;

namespace Application.Account.SendResetPasswordEmail_Forgot_Password_;

public class SendResetPasswordCommandValidator:AbstractValidator<SendResetPasswordCommand>
{
    public SendResetPasswordCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Adres email nie może być pusty")
            .EmailAddress().WithMessage("Podaj prawidłowy adres email");
    }
}