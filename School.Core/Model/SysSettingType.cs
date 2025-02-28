namespace School.Core.Model;

public class SysSettingType
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<SysSetting> SysSettings { get; set; }
}