using FluentValidation;
using School.API.Contracts.Teacher;

namespace School.API.Validations.Teacher;

public class UpdateTeacherValidator : AbstractValidator<UpdateTeacherRequest>
{
    public UpdateTeacherValidator()
    {
        RuleFor(t => t.Id).NotNull().NotEqual(Guid.Empty);
        RuleFor(t => t.FirstName).NotNull().NotEmpty().Length(3,50);
        RuleFor(t => t.MiddleName).NotNull().NotEmpty().Length(3,50);
        RuleFor(t => t.LastName).NotNull().NotEmpty().Length(3,50);
        RuleFor(t => t.BirthDate).GreaterThanOrEqualTo(DateTime.Now.AddYears(-18));
        RuleFor(t => t.Sex).NotEmpty().NotNull();
    }
}