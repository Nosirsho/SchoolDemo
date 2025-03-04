using FluentValidation;
using School.API.Contracts.Schedule;

namespace School.API.Validations.Schedule;

public class LessonNumberRequestValidator : AbstractValidator<LessonNumberRequest>
{
    public LessonNumberRequestValidator()
    {
        RuleFor(x => x.Number).GreaterThanOrEqualTo(1).LessThanOrEqualTo(8);
        RuleFor(x => x.LessonId).NotNull().NotEmpty();
    }
}