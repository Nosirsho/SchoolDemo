using FluentValidation;
using School.API.Contracts.Lesson;

namespace School.API.Validations.Lesson;

public class UpdateLessonValidator: AbstractValidator<UpdateLessonRequest>
{
    public UpdateLessonValidator()
    {
        RuleFor(l => l.Id).NotNull().NotEqual(Guid.Empty);
        RuleFor(l => l.Name).NotNull();
    }
}