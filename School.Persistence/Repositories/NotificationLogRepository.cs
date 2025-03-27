using AutoMapper;
using School.Core.Model;
using School.Core.Stores;
using School.Persistence.Entities;

namespace School.Persistence.Repositories;

public class NotificationLogRepository : INotificationLogStore
{
    private readonly SchoolDbContext _schoolDbContext;
    private readonly IMapper _mapper;

    public NotificationLogRepository(SchoolDbContext schoolDbContext, IMapper mapper)
    {
        _schoolDbContext = schoolDbContext;
        _mapper = mapper;
    }
    public async Task SaveLog(NotificationLog notificationLog)
    {
        var notificationLogEntity = _mapper.Map<NotificationLogEntity>(notificationLog);
        await _schoolDbContext.NotificationLogs.AddAsync(notificationLogEntity);
        await _schoolDbContext.SaveChangesAsync();
    }
}