using FluentValidation;
using School.API.Contracts.Parent;

namespace School.API.Validations.Parent;

public class UpdateParentValidator : AbstractValidator<UpdateParentRequest>
{
    public UpdateParentValidator()
    {
        RuleFor(p  => p.Id).NotNull().NotEqual(Guid.Empty);
        RuleFor(p  => p.FirstName).NotNull().NotEmpty().Length(3,50);
        RuleFor(p  => p.MiddleName).NotNull().NotEmpty().Length(3,50);
        RuleFor(p  => p.LastName).NotNull().NotEmpty().Length(3,50);
        RuleFor(p  => p.Sex).NotEmpty().NotNull();
    }
}