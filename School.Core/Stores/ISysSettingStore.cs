using School.Core.Model;

namespace School.Core.Stores;

public interface ISysSettingStore
{
    Task<Guid> CreateSysSetting(string name, string code, Guid typeId, string value);
    Task<SysSetting> UpdateSysSetting(Guid id, string name, string code, Guid typeId, string value);
    Task<string> GetValueByCode(string code);
    Task<IReadOnlyList<SysSetting>> GetAll();
    Task<SysSetting> GetById(Guid id);
}