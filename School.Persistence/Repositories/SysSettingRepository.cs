using System.Reflection.Metadata;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using School.Core.Constants;
using School.Core.Model;
using School.Core.Stores;
using School.Persistence.Entities;

namespace School.Persistence.Repositories;

public class SysSettingRepository : ISysSettingStore
{
    private readonly SchoolDbContext _schoolDbContext;
    private readonly IMapper _mapper;

    public SysSettingRepository( SchoolDbContext schoolDbContext, IMapper mapper )
    {
        _schoolDbContext = schoolDbContext;
        _mapper = mapper;
    }
    public async Task<string> GetValueByCode(string code)
    {
        var sysSettingEntity = await _schoolDbContext.SysSettings.FirstOrDefaultAsync(s => s.Code == code);
        
        if (sysSettingEntity == null)
        {
            throw new KeyNotFoundException($"SysSetting with code { code } not found");
        }

        if (sysSettingEntity.SysSettingTypeId == BaseConstant.SysSettingType.String)
        {
            return sysSettingEntity.StringValue;
        } else if (sysSettingEntity.SysSettingTypeId == BaseConstant.SysSettingType.Integer)
        {
            return sysSettingEntity.IntegerValue.ToString();
        } else if (sysSettingEntity.SysSettingTypeId == BaseConstant.SysSettingType.Boolean)
        {
            return sysSettingEntity.BooleanValue.ToString();
        }
        else if (sysSettingEntity.SysSettingTypeId == BaseConstant.SysSettingType.DateTime)
        {
            return sysSettingEntity.DateTimeValue.ToString("yyyy-MM-dd");
        }
        else if (sysSettingEntity.SysSettingTypeId == BaseConstant.SysSettingType.Guid)
        {
            return sysSettingEntity.GuidValue.ToString();
        }
        else
        {
            return string.Empty;
        }
    }

    public async Task<Guid> CreateSysSetting(string code, Guid typeId, string value)
    {
        var sysSetting = await _schoolDbContext.SysSettings.FirstOrDefaultAsync(s => s.Code == code);
        if (sysSetting != null)
        {
            throw new KeyNotFoundException($"SysSetting with code { code } exists!");
        }

        var sysSettinfType = await _schoolDbContext.SysSettingType.FirstOrDefaultAsync(s => s.Id == typeId);
        if (sysSettinfType == null)
        {
            throw new KeyNotFoundException($"SysSetting with type { typeId } not found!");
        }

        var sysSettingEntity = new SysSettingEntity()
        {
            Id = new Guid(),
            Code = code,
            SysSettingTypeId = sysSettinfType.Id,
        };
        if (sysSettinfType.Id == BaseConstant.SysSettingType.String)
        {
            sysSettingEntity.StringValue = value;
        } else if (sysSettinfType.Id == BaseConstant.SysSettingType.Integer)
        {
            sysSettingEntity.IntegerValue = int.Parse(value);
        } else if (sysSettinfType.Id == BaseConstant.SysSettingType.Boolean)
        {
            sysSettingEntity.BooleanValue = bool.Parse(value);
        } else if (sysSettinfType.Id == BaseConstant.SysSettingType.DateTime)
        {
            sysSettingEntity.DateTimeValue = DateTime.Parse(value).ToUniversalTime();
        } else if (sysSettinfType.Id == BaseConstant.SysSettingType.Guid)
        {
            sysSettingEntity.GuidValue = Guid.Parse(value);
        } else {
            throw new KeyNotFoundException($"SysSetting with type { typeId } not found!");
        }
            
        await _schoolDbContext.AddAsync(sysSettingEntity);
        await _schoolDbContext.SaveChangesAsync();
        return sysSettingEntity.Id;
    }

    public async Task<IReadOnlyList<SysSetting>> GetAll()
    {
        var sysSettingEntities = await _schoolDbContext.SysSettings.Include(ss=>ss.Type).ToListAsync();
        return _mapper.Map<IReadOnlyList<SysSetting>>(sysSettingEntities);
    }

    public async Task<SysSetting> GetById(Guid id)
    {
        var result = await _schoolDbContext.SysSettings
            .Include(ss=>ss.Type)
            .FirstOrDefaultAsync(s => s.Id == id);
        return _mapper.Map<SysSetting>(result);
    }
}