using School.Core.Enums;

namespace School.Core.Model;

public class SysSetting
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; }
    public SysSettingType Type { get; set; }
    public Guid TypeId { get; set; }
    public int IntegerValue { get; set; }
    public DateTime DateTimeValue { get; set; }
    public bool BooleanValue { get; set; }
    public string StringValue { get; set; } = string.Empty;
    public Guid GuidValue { get; set; }
}

