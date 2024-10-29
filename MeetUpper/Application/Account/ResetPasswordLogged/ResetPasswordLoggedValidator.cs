using FluentValidation;

namespace Application.Account.ResetPasswordLogged;

public class ResetPasswordLoggedValidator:AbstractValidator<ResetPasswordLoggedCommand>
{
    public ResetPasswordLoggedValidator()
    {
        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("Podaj hasło")
            .Matches(@"(?=.*[A-Z].*[A-Z]?)").WithMessage("Hasło musi zawierać dużą literę")
            .Matches(@"(?=.*[a-z].*[a-z]?)").WithMessage("Hasło musi zawierać małą literę")
            .Matches(@"(?=.*\d.*\d?)").WithMessage("Hasło musi zawierać cyfrę")
            .Matches(@"(?=.*[\$%&@#].*[\$%&@#]?)").WithMessage("Hasło musi zawierać znak alfanumeryczny")
            .MinimumLength(8).WithMessage("Hasło musi zawierać minimum 8 znaków");
        RuleFor(x => x.OldPassword).NotEmpty().WithMessage("Podaj stare hasło");
    }
}