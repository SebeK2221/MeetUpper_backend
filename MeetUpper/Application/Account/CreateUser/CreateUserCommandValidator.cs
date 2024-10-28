using FluentValidation;

namespace Application.Account.CreateUser;

public class CreateUserCommandValidator:AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Panie podaj pan imie");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Podaj naziwsko");
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Podaj numer telefonu")
            .Matches(@"^\d{11}$").WithMessage("Podaj numer telefonu z prefiksem");
        RuleFor(x => x.RepeatPassword).Equal(x => x.Password).WithMessage("Hasła są inne");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Podaj hasło")
            .Matches(@"(?=.*[A-Z].*[A-Z]?)").WithMessage("Hasło musi zawierać dużą literę")
            .Matches(@"(?=.*[a-z].*[a-z]?)").WithMessage("Hasło musi zawierać małą literę")
            .Matches(@"(?=.*\d.*\d?)").WithMessage("Hasło musi zawierać cyfrę")
            .Matches(@"(?=.*[\$%&@#].*[\$%&@#]?)").WithMessage("Hasło musi zawierać znak alfanumeryczny")
            .MinimumLength(8).WithMessage("Hasło musi zawierać minimum 8 znaków");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Adres email nie może być pusty")
            .EmailAddress().WithMessage("Podaj poprawny adres email");
    }
}