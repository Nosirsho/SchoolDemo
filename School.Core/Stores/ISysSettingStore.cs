namespace School.Core.Stores;

public interface ISysSettingStore
{
    Task<string> GetValueByCode(string code);
    Task<Guid> CreateSysSetting(string code, Guid typeId, string value);
}