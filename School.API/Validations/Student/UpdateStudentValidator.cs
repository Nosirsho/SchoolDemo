using FluentValidation;
using School.API.Contracts.Student;

namespace School.API.Validations.Student;

public class UpdateStudentValidator : AbstractValidator<UpdateStudentRequest>
{
    public UpdateStudentValidator()
    {
        RuleFor(s => s.Id).NotNull().NotEqual(Guid.Empty);
        RuleFor(s => s.FirstName).NotNull().NotEmpty().Length(3,50);
        RuleFor(s => s.MiddleName).NotNull().NotEmpty().Length(3,50);
        RuleFor(s => s.LastName).NotNull().NotEmpty().Length(3,50);
        RuleFor(s => s.BirthDate).GreaterThanOrEqualTo(DateTime.Now.AddYears(-18));
        RuleFor(s=>s.Sex).NotEmpty().NotNull();
    }
}