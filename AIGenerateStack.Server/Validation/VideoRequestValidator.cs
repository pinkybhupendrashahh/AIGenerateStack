using Domain.Models;
using FluentValidation;

namespace AIGenerateStack.Server.Validation
{
    public class VideoRequestValidator : AbstractValidator<VideoRequest>
    {
        public VideoRequestValidator()
        {
            RuleFor(x => x.Topic)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.LengthSec)
                .InclusiveBetween(5, 120);

            RuleFor(x => x.Style)
                .NotEmpty();

            RuleFor(x => x.Voice)
                .NotEmpty();
        }
    }

}
