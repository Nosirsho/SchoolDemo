using FluentValidation;
using School.API.Contracts.Student;

namespace School.API.Validations.Student;

public class CreateStudentValidator : AbstractValidator<CreateStudentRequest>
{
    public CreateStudentValidator()
    {
        RuleFor(s=>s.FirstName).Length(3,50).NotEmpty();
        RuleFor(s=>s.MiddleName).Length(3,50).NotEmpty();
        RuleFor(s=>s.LastName).Length(3,50).NotEmpty();
        RuleFor(s=>s.BirthDate).GreaterThan(DateTime.Now.AddYears(-18)).LessThan(DateTime.Now.AddYears(-7));
        RuleFor(s=>s.Sex).NotEmpty().NotNull();
    }
}