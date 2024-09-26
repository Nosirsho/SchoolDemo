using FluentValidation;
using School.API.Contracts.Parent;

namespace School.API.Validations.Parent;

public class CreateParentValidator : AbstractValidator<CreateParentRequest>
{
    public CreateParentValidator()
    {
        RuleFor(p => p.FirstName).Length(3,50).NotEmpty();
        RuleFor(p => p.MiddleName).Length(3,50).NotEmpty();
        RuleFor(p => p.LastName).Length(3,50).NotEmpty();
        RuleFor(p => p.Sex).NotEmpty().NotNull();
        RuleFor(p => p.Phone).NotEmpty().NotNull();
    }
}