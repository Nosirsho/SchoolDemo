using FluentValidation;
using School.API.Contracts.Schedule;

namespace School.API.Validations.Schedule;

public class DayLessonRequestValidator : AbstractValidator<DayLessonRequest>
{
    public DayLessonRequestValidator()
    {
        RuleFor(x => x.DayInt).GreaterThanOrEqualTo(1).LessThanOrEqualTo(7);
        RuleForEach(x => x.LessonNumbers).SetValidator(new LessonNumberRequestValidator());
    }
}