using FluentValidation;
using School.API.Contracts.GradeLevel;
using School.API.Contracts.Teacher;

namespace School.API.Validations.GradeLevel;

public class UpdateGradeLevelValidator : AbstractValidator<UpdateGradeLevelRequest>
{
    public UpdateGradeLevelValidator()
    {
        RuleFor(gl => gl.Id).NotNull().NotEqual(Guid.Empty);
        RuleFor(gl => gl.Name).NotNull().Length(2, 3);
    }
}

