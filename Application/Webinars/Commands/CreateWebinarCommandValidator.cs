using FluentValidation;

namespace Application.Webinars.Commands;

public class CreateWebinarCommandValidator : AbstractValidator<CreateWebinarCommand>
{
    public CreateWebinarCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(x => x.ScheduledOn)
            .NotNull()
            .WithMessage("");
    }
}
