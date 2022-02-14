using FluentValidation;

namespace ProjektAPI.Model
{
    public class RegisterUserValidator : AbstractValidator<RegisterUser>
    {
        public RegisterUserValidator(WeatherDataContext db)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .MinimumLength(6);

            RuleFor(x => x.ConfirmPasswoed)
                .Equal(x => x.Password);

            RuleFor(x => x.Email).Custom((value, context) =>
            {
                var emailInUse = db.Users.Any(x => x.Email == value);
                if (emailInUse)
                {
                    context.AddFailure("Email", "Ten email jest wykorzystany");
                }
            });
        }
    }
}
