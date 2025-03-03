using School.Core.Model;
using School.Core.Stores;

namespace School.Application.Services;

public class SysSettingService
{
    private readonly ISysSettingStore _sysSettingStore;

    public SysSettingService(ISysSettingStore sysSettingStore)
    {
        _sysSettingStore = sysSettingStore;
    }

    public async Task<string> GetSysSettingValueByCode(string code)
    {
        return await _sysSettingStore.GetValueByCode(code);
    }

    public async Task<Guid> CreateSysSetting(string name, string code, Guid typeId, string value)
    {
        return await _sysSettingStore.CreateSysSetting(name, code, typeId, value);
    }
    
    public async Task<SysSetting> UpdateSysSetting(Guid id, string name, string code, Guid typeId, string value)
    {
        return await _sysSettingStore.UpdateSysSetting(id, name, code, typeId, value);
    }
    public async Task<IReadOnlyList<SysSetting>> GetSysSettingList()
    {
        return await _sysSettingStore.GetAll();
    }

    public async Task<SysSetting> GetSysSettingById(Guid id)
    {
        return await _sysSettingStore.GetById(id);
    }

}