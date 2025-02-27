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

    public async Task<Guid> CreateSysSetting(string code, Guid typeId, string value)
    {
        return await _sysSettingStore.CreateSysSetting(code, typeId, value);
    }
    
    public async Task<ICollection<SysSetting>> GetSysSettingList()
    {
        return await _sysSettingStore.GetAll();
    }

}