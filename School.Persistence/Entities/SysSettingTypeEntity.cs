namespace School.Persistence.Entities;

public class SysSettingTypeEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<SysSettingEntity> SysSettings { get; set; }
}