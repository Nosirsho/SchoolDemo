using FluentValidation;
using School.API.Contracts.Schedule;

namespace School.API.Validations.Schedule;

public class CreateScheduleValidator : AbstractValidator<CreateScheduleRequest>
{
    public CreateScheduleValidator()
    {
        RuleFor(s=>s.DayOfWeek).NotNull();
        RuleFor(s=>s.TeacherId).NotNull().NotEqual(Guid.Empty);
        RuleFor(s=>s.LessonId).NotNull().NotEqual(Guid.Empty);
        RuleFor(s=>s.GradeLevelId).NotNull().NotEqual(Guid.Empty);
        
    }
}