namespace School.API.Contracts.SysSetting;

public record CreateSysSettingRequest(string Code, Guid TypeId, string Value);