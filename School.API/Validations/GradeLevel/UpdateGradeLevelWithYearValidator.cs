using FluentValidation;
using School.API.Contracts.GradeLevel;
namespace School.API.Validations.GradeLevel;

public class UpdateGradeLevelWithYearValidator : AbstractValidator<UpdateGradeLevelWithYearRequest>
{
    public UpdateGradeLevelWithYearValidator()
    {
        RuleFor(gl => gl.Id).NotNull().NotEqual(Guid.Empty);
        RuleFor(gl => gl.Name).NotNull().Length(2, 3);
    }
}
