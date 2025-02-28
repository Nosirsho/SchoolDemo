using School.Core.Model;

namespace School.Core.Stores;

public interface ISysSettingStore
{
    Task<string> GetValueByCode(string code);
    Task<Guid> CreateSysSetting(string code, Guid typeId, string value);
    Task<IReadOnlyList<SysSetting>> GetAll();
    Task<SysSetting> GetById(Guid id);
}