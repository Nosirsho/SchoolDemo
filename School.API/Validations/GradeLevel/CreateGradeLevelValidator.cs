using FluentValidation;
using School.API.Contracts.GradeLevel;

namespace School.API.Validations.GradeLevel;

public class CreateGradeLevelValidator : AbstractValidator<CreateGradeLevelRequest>
{
    public CreateGradeLevelValidator()
    {
        RuleFor(gl=>gl.Name).Length(2,3).NotEmpty();
    }
}