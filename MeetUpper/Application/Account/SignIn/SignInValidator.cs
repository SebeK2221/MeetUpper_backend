using FluentValidation;

namespace Application.Account.SignIn;

public class SignInValidator:AbstractValidator<SignInCommand>
{
    public SignInValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Podaj adres email")
            .EmailAddress().WithMessage("Niepoprawny adres email");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Podaj hasło")
            .Matches(@"(?=.*[A-Z].*[A-Z]?)").WithMessage("Hasło musi zawierać dużą literę")
            .Matches(@"(?=.*[a-z].*[a-z]?)").WithMessage("Hasło musi zawierać małą literę")
            .Matches(@"(?=.*\d.*\d?)").WithMessage("Hasło musi zawierać cyfrę")
            .Matches(@"(?=.*[\$%&@#].*[\$%&@#]?)").WithMessage("Hasło musi zawierać znak alfanumeryczny")
            .MinimumLength(8).WithMessage("Hasło musi zawierać minimum 8 znaków");
    }
}