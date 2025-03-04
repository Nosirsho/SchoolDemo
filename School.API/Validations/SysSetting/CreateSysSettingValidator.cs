using FluentValidation;
using School.API.Contracts.SysSetting;

namespace School.API.Validations.SysSetting;

public class CreateSysSettingValidator: AbstractValidator<CreateSysSettingRequest>
{
    public CreateSysSettingValidator()
    {
        RuleFor(ss => ss.Name).NotEmpty().MaximumLength(50);
        RuleFor(ss => ss.Code).NotEmpty().MaximumLength(50);
        RuleFor(ss=>ss.TypeId).NotEqual(Guid.Empty).NotEmpty().NotNull();
        RuleFor(ss=>ss.Value).NotEmpty();
    }
}