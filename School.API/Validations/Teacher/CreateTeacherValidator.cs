using FluentValidation;
using School.API.Contracts.Teacher;

namespace School.API.Validations.Teacher;

public class CreateTeacherValidator : AbstractValidator<CreateTeacherRequest>
{
    public CreateTeacherValidator()
    {
        RuleFor(t=>t.FirstName).Length(3,50).NotEmpty();
        RuleFor(t=>t.MiddleName).Length(3,50).NotEmpty();
        RuleFor(t=>t.LastName).Length(3,50).NotEmpty();
        RuleFor(t=>t.Sex).NotEmpty().NotNull();
        RuleFor(t=>t.Phone).NotEmpty().NotNull();
    }
}