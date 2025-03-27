using School.Core.Model;

namespace School.Core.Stores;

public interface INotificationLogStore
{
    Task SaveLog(NotificationLog notificationLog);
}