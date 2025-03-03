namespace School.API.Contracts.SysSetting;

public record CreateSysSettingRequest(string Name, string Code, Guid TypeId, string Value);