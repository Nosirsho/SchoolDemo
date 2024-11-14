using FluentValidation;
using School.API.Contracts.Schedule;

namespace School.API.Validations.Schedule;

public class ScheduleRequestValidator : AbstractValidator<ScheduleRequest>
{
    public ScheduleRequestValidator()
    {
        RuleFor(x => x.GradeLevelId).NotNull().NotEmpty();
        RuleForEach(x => x.DayLessons).SetValidator(new DayLessonRequestValidator());
    }
}