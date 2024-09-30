using FluentValidation;
using School.API.Contracts.Lesson;

namespace School.API.Validations.Lesson;

public class CreateLessonValidator : AbstractValidator<CreateLessonRequest>
{
    public CreateLessonValidator()
    {
        RuleFor(l=>l.Name).NotEmpty();
    }
}