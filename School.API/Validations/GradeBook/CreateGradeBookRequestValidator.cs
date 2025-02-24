using FluentValidation;
using School.API.Contracts.GradeBook;

namespace School.API.Validations.GradeBook;

public class CreateGradeBookRequestValidator: AbstractValidator<CreateGradeBookRequest>
{
    public CreateGradeBookRequestValidator()
    {
        RuleFor(l=>l.StudentId).NotNull().NotEqual(Guid.Empty);
        RuleFor(l=>l.LessonId).NotNull().NotEqual(Guid.Empty);
        RuleFor(l=>l.Grade).InclusiveBetween(1, 5);
        // RuleFor(l=>l.Date).Must(BeToday)
        //     .WithMessage("Дата должна быть сегодняшней.");
    }
    // private bool BeToday(DateTime date)
    // {
    //     return date.Date == DateTime.Today;
    // }
}