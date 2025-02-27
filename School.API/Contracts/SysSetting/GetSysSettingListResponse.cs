namespace School.API.Contracts.SysSetting;

public record GetSysSettingListResponse(
    Guid Id, 
    string Name, 
    string Code, 
    string Type, 
    int IntegerValue, 
    string DateValue,
    bool BooleanValue,
    string StringValue,
    Guid GuidValue
    );