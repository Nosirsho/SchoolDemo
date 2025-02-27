namespace School.Persistence.Entities;

public class SysSettingEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; }
    public SysSettingTypeEntity Type { get; set; }
    public Guid SysSettingTypeId { get; set; }
    public int IntegerValue { get; set; }
    public DateTime DateTimeValue { get; set; }
    public bool BooleanValue { get; set; }
    public string StringValue { get; set; } = string.Empty;
    public Guid GuidValue { get; set; }
}