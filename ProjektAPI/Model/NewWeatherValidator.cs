using FluentValidation;

namespace ProjektAPI.Model
{
    public class NewWeatherValidator : AbstractValidator<NewWeather>
    {
        public NewWeatherValidator(WeatherDataContext db)
        {
            RuleFor(x => x.Temperature).NotEmpty();
            RuleFor(x => x.Presure).NotEmpty();
            RuleFor(x => x.RainFall).GreaterThanOrEqualTo(0);
            RuleFor(x => x.CloudCover).InclusiveBetween(0.0f, 1.0f);
        }
    }
}
